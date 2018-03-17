# Introduction

These contribution guidelines were introduced to the project on 26th June 2017. It is acknowledged that much of the code which was committed prior to then does not met the standards set out here (herein after "legacy code").


# General Coding Standards

We attempt to abide by the following coding standards (listed in order of precedence):

* Project specific standards which are documented in this file.
* Release v5.0.0 of Dennis Doomen's [C# Guidelines](https://github.com/dennisdoomen/CSharpGuidelines). Note there is a [handy cheatsheet/aid memior](https://github.com/dennisdoomen/CSharpGuidelines/releases/tag/5.0.0).
* Microsoft's [.Net Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/index)

It is acceptable to deviate from these standards on two conditions; (a) the justification for deviating from the standards has been documented in the comments. (b) that justification has been accepted at least one other project member prior to it being merged into the master branch.


# Automated Tests

New code, bug fixes or heavily refactored code should include unit/automated tests. There are two circumstances were it is acceptable to not include unit/automated tests are:

* Small changes within legacy code. It is accepted that much of the legacy code does not lend itself to automated testing.
* Code directly related to the behaviour of the GUI. In this case it is important that as far as is practical GUI code is separated from “business code” and that the business code has automated tests. (Someone please suggest a better term than “business code”!)

We are not overly concerned about the formal distinctions between “unit tests” and “integration tests” and other definitions.  It is permissible to include example files in the `arcgis10_mapping_tools\CommonTests\testfiles` dir. These can be read within tests and output can be written to a temporary directory. The overall execution time of the test suite is more important and we should aim for a couple of minutes in total.


# Refactoring legacy code

There is a general effort to refractor legacy code (documented elsewhere). This will remove a lot of the static, procedural code with proper object orientated class structure. Some pertinent points related to code standards: 

* The new classes are expected to be instantiated for the duration of a form being visible. There is no need to maintain any state information about the open MXD etc.
* The classes can assume to be single threaded and that the underlying MXD, config files, maps etc will not change during the life of these objects.
* Once the refactoring complete it is likely that there should be very few or no references to the ESRI.ArcGIS classes from within the `frmExportMain`, `frmLayoutMain` or `frmConfigMain` classes. 


# Contributions from those outside MapAction

We would gladly welcome contributions from anyone outside MapAction. In particular we'd be keen on bug fixes or new functionality which is makes these tools more useful to the wider humanitarian information management community. To date all contributions have come from within MapAction, therefore if you are interested please open an issue or pull request to discuss your ideas. Many thanks.
