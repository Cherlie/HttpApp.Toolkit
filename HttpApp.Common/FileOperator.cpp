#include "pch.h"
#include "FileOperator.h"

using namespace HttpApp_Common;
using namespace Platform;
using namespace Windows::Storage;
using namespace Windows::Storage::Streams;
using namespace std;

FileOperator::FileOperator()
{
}

//C++�ļ���ȡ����,���첽����������ֻ�ܶ�ȡInstallationĿ¼������ļ�
String^ FileOperator::ReadFiles(String^ filename)
{
	auto folder = Windows::ApplicationModel::Package::Current->InstalledLocation;

	Array<byte>^ data = nullptr;
	std::wstring dir = folder->Path->ToString()->Data();
	auto fullpath = dir.append(L"/Assets/").append(filename->Data());

	ifstream file(fullpath, std::ios::in | std::ios::ate);
	// �ļ�����
	if (file.is_open())
	{
		int length = (int)file.tellg();
		data = ref new Array<byte>(length);
		file.seekg(0, std::ios::beg);
		file.read(reinterpret_cast<char*>(data->Data), length);
		file.close();
	}
	
	std::wstring output;
	for (int i = 0; i < data->Length; i++)
		output += data[i];

	return ref new String(output.c_str());
}