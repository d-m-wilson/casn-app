/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
:r .\dbo\Tables\Data\AppointmentType.data.sql
GO
:r .\dbo\Tables\Data\Badge.data.sql
GO
:r .\dbo\Tables\Data\DriveCancelReason.data.sql
GO
:r .\dbo\Tables\Data\DriveLogStatus.data.sql
GO
:r .\dbo\Tables\Data\DriveStatus.data.sql
GO
:r .\dbo\Tables\Data\Message.data.sql
GO
:r .\dbo\Tables\Data\ServiceProviderType.data.sql
GO
