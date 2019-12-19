[1]: https://github.com/yarseyah/sentinel
[2]: https://github.com/dojo90/NLogViewer/blob/master/src/NLogViewer/Targets/CacheTarget.cs
[3]: https://github.com/yarseyah/sentinel#nlogs-nlogviewer-target-configuration

[p1]: doc/images/control.png "NLogViewer"
[p2]: doc/images/live.gif "NLogViewer"
[p3]: doc/images/control2.png "NLogViewer"

NlogViewer
==========

NlogViewer is a ui control library to visualize NLog logs. It is mainly based on [Sentinel][1] and its controls.

Actually it contains the following controls:

- `NLogViewer`

Visual Studio
![NLogViewer][p1]
![NLogViewer][p3]

Live preview
![NLogViewer][p2]


## Quick Start

Add a namespace to your `Window`

```xaml
xmlns:dj="clr-namespace:DJ;assembly=NLogViewer"
```

use the control
```xaml
<dj:NLogViewer/>
```

`NlogViewer` is subscribing to [CacheTarget][2]. By default, the `NlogViewer` is automatically creating a [CacheTarget][2] with `loggingPattern  "*"` and `LogLevel "Trace"`.

If you want to customize the `loggingPattern` and `LogLevel`, add the following to your `Nlog.config`.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog 
  xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
  autoReload="true">

  <extensions> 
    <add assembly="NLogViewer"/> 
  </extensions> 

  <targets async="true">
    <target
      xsi:type="CacheTarget"
      name="cache"/>
  </targets>

  <rules>
    <logger name="*" writeTo="cache" minlevel="Debug"/> 
  </rules>
</nlog>
```

## Why CacheTarget?

There is already a `NLogViewerTarget`, which is used for [Sentinel][1]. See [here][3]

```xml
<target 
    xsi:type="NLogViewer"
    name="sentinel"
    address="udp://127.0.0.1:9999"/>
```

## Contributors

Feel free to make a PullRequest or open an Issue to extend this library!