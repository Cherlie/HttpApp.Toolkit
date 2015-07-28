// RenderImage.cpp
#include "pch.h"
#include "RenderImage.h"

using namespace HttpApp_Common;
using namespace Windows::UI::Xaml;
using namespace Platform;
using namespace concurrency;
using namespace Windows::UI::Xaml::Media::Imaging;
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::Storage;
using namespace Windows::Graphics::Imaging;
using namespace Windows::Foundation;

RenderImage::RenderImage()
{
}

//从XAML截获图片并将图片保存
task<void> RenderImage::RenderAndSaveToFileAsync(UIElement^ uiElement, String^ outputImageFilename, uint32 width, uint32 height)
{
	RenderTargetBitmap^ rtb = ref new RenderTargetBitmap();
	return create_task(rtb->RenderAsync(uiElement, width, height))
		.then([this, rtb]() -> IAsyncOperation<IBuffer^>^
	{
		this->pixelWidth = (uint32)rtb->PixelWidth;
		this->pixelHeight = (uint32)rtb->PixelHeight;
		return rtb->GetPixelsAsync();
	}).then([this, rtb, outputImageFilename](IBuffer^ buffer)
	{

		StorePixelsFromBuffer(buffer);
		return WriteBufferToFile(outputImageFilename);
	});
}

Array<unsigned char>^ RenderImage::GetArrayFromBuffer(IBuffer^ buffer)
{
	Streams::DataReader^ dataReader = Streams::DataReader::FromBuffer(buffer);
	Array<unsigned char>^ data = ref new Array<unsigned char>(buffer->Length);
	dataReader->ReadBytes(data);
	return data;
}

void RenderImage::StorePixelsFromBuffer(IBuffer^ buffer)
{
	this->pixelData = GetArrayFromBuffer(buffer);
}

task<void> RenderImage::WriteBufferToFile(String^ outputImageFilename)
{
	auto resultStorageFolder = ApplicationData::Current->LocalFolder;

	return create_task(resultStorageFolder->CreateFileAsync(outputImageFilename, CreationCollisionOption::ReplaceExisting)).
		then([](StorageFile^ outputStorageFile) ->IAsyncOperation<IRandomAccessStream^>^
	{
		return outputStorageFile->OpenAsync(FileAccessMode::ReadWrite);
	}).then([](IRandomAccessStream^ outputFileStream) ->IAsyncOperation<BitmapEncoder^>^
	{
		return BitmapEncoder::CreateAsync(BitmapEncoder::PngEncoderId, outputFileStream);
	}).then([this](BitmapEncoder^ encoder)->IAsyncAction^
	{
		encoder->SetPixelData(BitmapPixelFormat::Bgra8, BitmapAlphaMode::Premultiplied, this->pixelWidth, this->pixelHeight, 96, 96, this->pixelData);
		return encoder->FlushAsync();
	}).then([this]()
	{
		this->pixelData = nullptr;
		return;
	});
}

//CX的异步处理，这样才能被C#调用
IAsyncAction^ RenderImage::RenderImageToFileAsync(UIElement^ uiElement, String^ outputImageFilename)
{
	return create_async([this, uiElement, outputImageFilename]()
	{
		return this->RenderAndSaveToFileAsync(uiElement, outputImageFilename, (uint32)uiElement->DesiredSize.Width, (uint32)uiElement->DesiredSize.Height);
	});
}