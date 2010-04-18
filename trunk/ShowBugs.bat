@echo off
for %%n in (9 8 7 6 5 4 3 2 1) do for /r . %%v in (*.cs) do find /n /i "(priority %%n)" "%%~fv" > null && find /n /i "(priority %%n)" "%%~fv"