@echo off

DEL "%APPDATA%\nsync\*" /s /q /f

msiexec /x {4F2F382E-3D60-4304-8D3C-68A808D9C1DB}