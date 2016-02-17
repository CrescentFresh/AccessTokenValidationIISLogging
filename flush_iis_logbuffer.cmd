@echo off
echo Flushing iis log buffer to disk...
netsh http flush logbuffer
pause