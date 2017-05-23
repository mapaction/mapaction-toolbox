MapAction Toolbox
=================
MapAction have created and continue to maintain a small collection of tools, to
help streamline the process of producing maps in the during emergency
operations. These are predominately focused on working with ESRI ArcGIS
products.

MapAction is a not-for-profit organisation which provides a mapping information
service at the scene of any major disaster. Using techniques including
geographical positioning systems (GPS), satellite communications, geographical
information systems (GIS), and personal observation, MapAction produce maps
depicting the dynamic situation. These are freely distributed to relief agencies
and are available on our website.

Building
========
Interactive builds requirements:
- ArcMap Desktop v10.1 or v10.2.2 (standard edition or higher) installed and licenced
- ArcMap SDK (ArcObjects) same version as Desktop
- Visual Studio 2008 or later
- NUnit v2.6.4

Commandline build requirements:
- ArcMap Desktop v10.2.2 (standard edition or higher) installed and licenced
- ArcMap SDK (ArcObjects) v10.2.2
- Visual Studio Professional 2010 SP1
- Visual Studio 2010 SP1 SDK 1.1
- NUnit v2.6.4

For commandline build to work correctly some post install actions are required. See [this stackexchange answer](http://gis.stackexchange.com/a/154880/34520) for details. 

```winbatch
echo about to set VS env
call "C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\bin\vcvars32.bat"
echo done VS env
echo about to reg nunit.framework
"c:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe" /silent /i "C:\Program Files (x86)\NUnit 2.6.4\bin\framework\nunit.framework.dll"
echo done reg nunit.framework
echo about to reg Microsoft.VisualStudio.Shell.9.0.dll
"c:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe" /silent /i "C:\Program Files (x86)\Microsoft Visual Studio 2010 SDK SP1\VisualStudioIntegration\Common\Assemblies\v2.0\Microsoft.VisualStudio.Shell.9.0.dll"
echo done reg Microsoft.VisualStudio.Shell.9.0.dll
```

About
=====
Copyright (c) 2016 [MapAction](https://mapaction.org).

The development of version 4 of the MapAction Toolbox was generously funded by [ECHO](http://ec.europa.eu/echo).

![alt text][echologo]

[echologo]: http://www.echo-visibility.eu/wp-content/uploads/2014/02/EU_Flag_HA_2016_EN-300x272.png "Funded by European Union Humanitarian Aid"


