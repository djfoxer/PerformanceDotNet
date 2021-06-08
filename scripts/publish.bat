REM delete old artifact folder
rmdir /s/q artifact
mkdir artifact
REM build app
dotnet publish "../src/djfoxer.PerformanceDotNet/djfoxer.PerformanceDotNet.App" -c Release -o artifact/bin/exe -f net5.0 -r win-x64 -p:PublishSingleFile=true
REM copy src
Xcopy /S /I /E /Q "../src/djfoxer.PerformanceDotNet" "artifact/bin" /exclude:xcopy_exclude.txt
REM create run.bat script
echo .\bin\exe\djfoxer.PerformanceDotNet.App.exe > artifact/run.bat
REM open artifact folder
cd artifact
REM get exe version
set exeVersion=""
FOR /f "tokens=3delims=<>" %%a IN ('findstr "Version" ".\bin\djfoxer.PerformanceDotNet.App\djfoxer.PerformanceDotNet.App.csproj"') DO set exeVersion=%%a
REM zip artifact content
tar.exe -avcf djfoxer.PerformanceDotNet_%exeVersion%.zip bin run.bat
REM open root folder
cd ..
REM remove old zip file
del djfoxer.PerformanceDotNet_*.zip
REM move new zip file to root
move artifact\djfoxer.PerformanceDotNet_%exeVersion%.zip


