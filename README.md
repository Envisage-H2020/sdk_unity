# Test setup for osx

## Export Paths
Standard installation directory of Mono.Framework.

`export PATH=$PATH:/Library/Frameworks/Mono.framework/Versions/<UNITY_VERSION>/bin`

## Compile Tests

`/Applications/Unity/PlaybackEngines/WebGLSupport/Managed/` -> This the standard location of the unityengine `dlls`.

Example:

mcs -target:library -r:nunit.framework -r:/Applications/Unity/PlaybackEngines/WebGLSupport/Managed/UnityEngine.dll -r:/Applications/Unity/PlaybackEngines/WebGLSupport/Managed/UnityEngine.CoreModule.dll -r:/Applications/Unity/PlaybackEngines/WebGLSupport/Managed/UnityEngine.UnityWebRequestWWWModule.dll -out:Tests.dll *.cs

Run Tests:
After compiling you can run the tests.dll
`nunit-console Tests.dll`
