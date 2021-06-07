rmdir /s/q artifact
mkdir artifact
dotnet publish "../src/djfoxer.PerformanceDotNet/djfoxer.PerformanceDotNet.App" -c Release -o artifact/bin/exe -f net5.0 -r win-x64 -p:PublishSingleFile=true
Xcopy /S /I /E /Q "../src/djfoxer.PerformanceDotNet" "artifact/bin" /exclude:xcopy_exclude.txt
echo .\bin\exe\djfoxer.PerformanceDotNet.App.exe > artifact/run.bat
cd artifact
tar.exe -avcf djfoxer.PerformanceDotNet_.zip bin run.bat
cd ..
del djfoxer.PerformanceDotNet_*.zip
move artifact\djfoxer.PerformanceDotNet_.zip



