# Rashell - Command Processor
Rashell is an Open-Source command processor for Windows.
It takes cues from the Windows Command Promot (CMD) and from Bash Shell <code>bash</code>.

Developers
--------------------------------------------
[![jcplaboratory](https://www.jcplaboratory.org/wp-content/uploads/2016/08/nav-banner_ra_large.png?w=250)](http://jcplaboratory.org)

Rashell is developed by the J.C.P Laboratory <http://jcplaboratory.org>.
The main developer's are:

* POOTTAREN J. Cédric
* BAICHOO Arwin Neil

Introduction
--------------------------------------------

Rashell is developed using Microsoft .NET Framework
Currently, .NET Framework 4.6 is required to run <code>Rashell</code>.

Recompiling using an older version of .NET should not be a problem.
<code>Rashell</code> is created to be fully dynamic and customizable.
Every behaviour of the Shell can be control from a configuration file <code>config.conf</code>. 
that is found in the startup directory of <code>Rashell</code>.


Licence
--------------------------------------------

Rashell is licenced under the CPGSL License Version 4.

[![cpgslv4](https://i0.wp.com/www.jcplaboratory.org/wp-content/uploads/2016/08/CPGSL-V4-400x.png?w=100)](https://www.jcplaboratory.org/products/cpgsl-version-4/)

Comparison between 'Rashell' and 'cmd'
--------------------------------------------

* Rashell is fully configurable and user customizable.
* Rashell supports custom Environment Paths that aren't specifically registered on the machine througth the <code>config.conf</code> file.
* Rashell is able to execute user defined executables.
* Rashell supports event and error loggin (We're still working on that one).
* Rashell is opensource.

Download Rashell
--------------------------------------------

Either you clone and compile this repository or clink on the link below.
https://github.com/jcplaboratory/rashell/blob/master/Rashell/bin/Debug/Rashell.exe

Execution via Argument
--------------------------------------------
Rashell supports execution via argument.

Here is a list of the currently support switches:

* `-c` This switch tells Rashell that it/she should execute the command specified. (Example: `rashell.exe -c mkdir %userprofile%\folder`).
* `-e` By default Rashell exits after the command parsed via arguments ends. To prevent that use the switch `-e`. (Example: `rashell.exe -ce mkdir %userprofile%\folder`).
* `-v` By default Rashell doesn't display the command it executes that are parsed via arguments. To make it (she) show the commands use the version (-v) switch. (`Example: rashell.exe -cev mkdir %userprofile%\folder`).

Built-in commands
--------------------------------------------

Rashell has number of buit-in commands.

Here's a list:

* `ls` - List all files and directory. Help command (`ls -h`).
* `mkdir` - Creates a new directory.
* `cd` - Sets the Working Current Directory.
* `pwd` - Prints the Current Working Directory.
* `echo` - Prints a string of characters on screen.
* `whoiam` -An enhanced versions of `whoami`.
* `clear` - Clears the console's screen
* `time` - Displays the current time.
* `date` - Displays the current date.
* `exit` & `bye` - Exits the Shell.

There's more to come.



