# Building

## Prerequisites (All Platforms)
* [CMake 3.5](https://cmake.org/) or newer
* C++11 compiler *(see platform specific requirements)*
* [Apache Geode](http://geode.apache.org/releases/) binaries installed or available to link against

   Running requires access to an installation of Geode. By default the value of `GEODE_HOME` is used as the path for the startserver.sh script, if gfsh is not in the path.

## Prequisites (Windows)

* Visual Studio 2015

## Steps to build

**Mac OS X / \*nix / Windows**

    $ cd <example>
    $ mkdir build
    $ cd build
    $ cmake ..
    $ cmake --build . -- <optional parallelism parameters>

## Steps to run

**Mac OS X / \*nix**

    $ cd <example>
    $ export DYLD_LIBRARY_PATH=$DYLD_LIBRARY_PATH:<install path>/geode-native/lib
    $ sh ./startserver.sh
    $ build/<example name>
    $ sh ./stopserver.sh

## Generator
CMake uses a "generator" to produce configuration files for use by a variety of build tools, e.g., UNIX makefiles, Visual Studio projects. By default a system-specific generator is used by CMake during configuration. (Please see [the CMake documentation](https://cmake.org/documentation/) for further information.) However, in many cases there is a better choice.

### Mac OS X / *nix Generator
The default generator:

    $ cmake ..

### Building requirements
Please see the documentation for building Geode Native.