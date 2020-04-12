# AIM Import

AIM Import is used to import configuration from AIM into the KCS system.
It is used in retrofit projects in order to produce mimics based on Flow View, Generic Image
    Variants (for Safety), and Trends.
It is responsible for making the configuration in the OMM complete, based on different rules that
    must be described for each project.
This is described in more detail later.

More details of the overview and architecture can be found at the [overview and
    architecture](overview.md) page (no longer up to date).

# The import process

1. Establish AIM Import configuration.
1. Update [settings.ini](settings.md) and other configuration files, like [symbol mappings](symbol-mapping.md).
1. Run `Duplicated tags checker` to make sure you don't have any duplicated tag names.
1. Run `UpdateMimicXml` to update the `mimic xml` file.
1. Run `UpdateServerConfiguration` to update the OMM
   - Setting up the [config.kmosx](kmosx.md) file 
1. Run `DWG Viewer` if you want to inspect the safety DWG files.
1. Run `Generate Symbol XAML` to generate resource `dlls` that can be used inside mimics (DWG files etc.).
1. Run `ConvertToMimics` to generate mimic files (safety, flow views, or both)
1. Find out that something is not right, change your settings and your configuration and try again.

## Essential reading
- [The mimic XML file](mimicxml.md). The mimic XML file is in the center of AIM Import.
- See [config.kmosx](kmosx.md) for more information of how to set up the configuration startup file.
- [symbol mappings](symbol-mapping.md) and [settings.ini](settings.md) to learn about the configuration.
- [System setup](system-setup.md) to learn about how AIM Import setups the OMM.
- [Update server configuration](update-server-configuration.md) to learn about how to setup a server.
- [Troubleshooting tips'n tricks](troubleshooting.md)
- [Migration guide from AIM Import 2 to AIM Import 3](guide-2to3.md). Not started yet.

## Other resources
- [Convert to mimics](convert-to-mimics.md) if you want to learn about how to convert mimics.
- [Creating safety for LSD (Large screen display)](safety-lsd.md) for info about creating mimics from visio files.

# Known Issues

This wiki is distributed with AIM Import and can therefore not be updated when we find new issues.
Please see [the old partner sharepoint
    wiki](http://sharepoint.ptfs.partner.master.int/Sites/BT/KCS-Automation/Team%20Wiki/AIM%20Import/Known%20issues.aspx)

* Use of **StrokeThicknessFactors** will affect both L2 and L3 mimics in Safety, and makes sometimes some text appear blurry or hard to read. This will be looked at later.
