## AwesomeLogger

Test Execution Command Line Tool (vstest.console.exe) pretty output logger

What is vstest.console.exe? Read [here] (http://blogs.msdn.com/b/bhuvaneshwari/archive/2012/06/16/vstest-console-exe-commandline-test-runner.aspx)

Usage:
1. Build using `build/build.cmd`, it will copy logger to `C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\Extensions\`
2. Edit `C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe.config` to include this logger
3. Run your tests with vstest.console.exe, logger will be automatically picked up
