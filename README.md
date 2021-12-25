# SimpleBrowser
A very basic browser using CefSharp. No fancy headers or tabs, just browsing. Perfect when using Curtains or WindowBlinds for styling

## Why?
When styling my Windows, especially when using styles like Irix or BeOS Themes, i recognized that the browsers are always a problem.
Either they do not apply the at all (like new FireFox) or look weird (Edge) or misbehave on some actions (Chrome).

So as that is really annoying because all i wanted was a clean browser window without fancy tabs, without tabs or url in the Titlebar and stuff like that i simply threw a CefSharp (aka Chromium-Browser) into a default Windows Form, added some buttons and logic and voila - just an extremely simple Browser that just does what it should and fits into my Windows-Style/Theme without any problems.
As a side effect, because no overloaded UI is loaded or rendered on the screen, starting the SimpleBrowser is really fast.

This is how it looks on BeOS5 Style
![Simple Browser in BeOS5 Style](https://github.com/MenNoWar/SimpleBrowser/blob/main/screenshots/beos5.png?raw=true)

or maybe a bit of El Capitain (WindowBlinds included default theme):
![Simple Browser in ElCapitain Style](https://github.com/MenNoWar/SimpleBrowser/blob/main/screenshots/osx.png?raw=true)

## Quirks
- As i do not implement Tabs in any way, when opening a new "Tab" either by clicking middle mouse button/mousewheel or hitting Ctrl+Click a new Window will be opened with the new Url.
- No Plugins like Adblocker or PopupBlocker
- Google will not accept a login as it is "not a supported Browser". Amazon, Netflix, DeivantArt, Disney+ and others do.

## Starting
You may either start the SimpleBrowser.exe without a parameter or provide the Url to open as Parameter.

Example: 
<pre>SimpeBrowser.exe http://www.youtube.com</pre>

will open a new Browser Window opening Youtube. This way you may simply add a link to your Panel (like RocketDock or Nexus WinStep).
