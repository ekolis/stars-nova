Option Explicit
On Error Resume Next

Const msiOpenDatabaseModeReadOnly = 0

Dim installer : Set installer = Wscript.CreateObject("WindowsInstaller.Installer") : CheckError
Dim installerPath : installerPath = Wscript.Arguments(2)
Dim database : Set database = installer.OpenDatabase(installerPath, msiOpenDatabaseModeReadOnly) : CheckError

Dim version : version = GetInstallerProperty("ProductVersion")
Set database = Nothing

Dim projectDir : projectDir = Wscript.Arguments(0)
Dim configuration : configuration = Wscript.Arguments(1)
Dim newFileName : newFileName = projectDir & configuration & "\stars-nova-" & version & "-windows.msi"

Wscript.Echo "newFileName  = " & newFileName

Dim fileSystem : Set fileSystem = CreateObject("Scripting.FileSystemObject")
fileSystem.MoveFile installerPath, newFileName : CheckError

Wscript.Quit 0


Function GetInstallerProperty(propertyName)
  Dim query : query = "SELECT Value FROM Property WHERE Property='" & propertyName & "'"
  Dim view : Set view = database.OpenView(query) : CheckError
  view.Execute : CheckError
  Dim record : Set record = view.Fetch
  If record Is Nothing Then Exit Function
  GetInstallerProperty = record.StringData(1)
End Function

Sub CheckError
  Dim message, errRec
  If Err = 0 Then Exit Sub
  message = Err.Source & " " & Hex(Err) & ": " & Err.Description
  If Not installer Is Nothing Then
    Set errRec = installer.LastErrorRecord
    If Not errRec Is Nothing Then message = message & vbLf & errRec.FormatText
  End If
  Fail message
End Sub

Sub Fail(message)
  Wscript.Echo message
  Wscript.Quit 2
End Sub