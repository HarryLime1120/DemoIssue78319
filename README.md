This repo tries to show the problem described in https://github.com/dotnet/runtime/issues/78319
"SerialPort ReadByte() does not longer raise the documented TimeoutException (after timeout reached) #78319"

github workflow added. Demonstrates the problem is also recreatable in the github cloud build & test environment.

Contains 2 csproj:
TargetFramework net7.0 and System.IO.Ports 7.0.0  => test FAILS because it raises an unexpected System.IO.IOException
TargetFramework net6.0 and System.IO.Ports 7.0.0  => test passes OK because it raises a TimeoutException as expected
