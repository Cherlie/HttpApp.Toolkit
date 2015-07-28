#pragma once

namespace HttpApp_Common
{
    public ref class RenderImage sealed
    {
	private:
		unsigned int pixelWidth;
		unsigned int pixelHeight;
		Platform::Array<unsigned char>^ pixelData;
		Platform::Array<unsigned char>^ GetArrayFromBuffer(Windows::Storage::Streams::IBuffer^ buffer);
		void RenderImage::StorePixelsFromBuffer(Windows::Storage::Streams::IBuffer^ buffer);
		concurrency::task<void> RenderImage::WriteBufferToFile(Platform::String^ outputImageFilename);
		concurrency::task<void> RenderAndSaveToFileAsync(Windows::UI::Xaml::UIElement^ uiElement, Platform::String^ outputImageFilename,
			uint32 width = 0, uint32 height = 0);
	public:
		RenderImage();
		Windows::Foundation::IAsyncAction^ RenderImageToFileAsync(Windows::UI::Xaml::UIElement^ uiElement, Platform::String^ outputImageFilename);
    };
}