#pragma once
namespace HttpApp_Common
{
 public ref	class FileOperator sealed
	{
	public:
		FileOperator();
		static Platform::String^ ReadFiles(Platform::String^  fileName);
	};

}