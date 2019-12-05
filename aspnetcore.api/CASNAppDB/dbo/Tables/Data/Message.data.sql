﻿BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()
    SET IDENTITY_INSERT [dbo].[Message] ON 
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (2, 1, N'We’ve got a one-way drive to {clinic} from {vagueTo} on {timeDate}. Can you help?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (3, 2, N'We’ve got a one-way drive to {vagueFrom} from {clinic} on {timeDate}. Can you help?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (4, 3, N'We’ve got a round-trip drive from {vagueTo} to {clinic}. Can you help?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (5, 4, N'We’ve got a round-trip drive from {vagueTo} to {clinic} returning to {vaugeFrom}. Can you help?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (6, 5, N'URGENT! We''ve got a caller that needs your help TODAY. Please sign into the app and be a hero!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (7, 6, N'Friends, Houstonians, countrymen, lend me your ears! The following rides still need to be filled for tomorrow. Let''s get them there!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (8, 7, N'It''s getting late, and these callers still need you. Can you sign up for any of these drives?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (9, 8, N'These folks need your help, and we are down to the wire. Please, please can you take one of these drives?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (10, 1, N'Hi {volunteerFirstName}, we’ve got a new one-way drive to {clinic} from {vagueTo} on {timeDate}. If you can help, sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (11, 1, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a one-way drive to {clinic} from {vagueTo} on {timeDate}, sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (12, 1, N'We’ve got a one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app and be a hero!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (13, 1, N'Hey {volunteerFirstName}, there''s a brand new one-way drive to {clinic} from {vagueTo} on {timeDate}. Can you get them there? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (14, 1, N'Happy {dayOfTheWeek}! Can you take a one-way drive to {clinic} from {vagueTo} on {timeDate}? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (15, 1, N'Hi {volunteerFirstName}, there''s a new one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app to make this caller''s day!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (16, 1, N'There''s a new one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app to get it while it''s hot!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (17, 1, N'Hi, {volunteerFirstName}! There''s a new one-way drive to {clinic} from {vagueTo} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (18, 1, N'Hi, {volunteerFirstName}! I''m a cute new one-way drive to {clinic} from {vagueTo} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (19, 1, N'Hi, {volunteerFirstName}! There''s a new one-way drive from {vagueTo} to {clinic} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride.', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (20, 1, N'Hi, {volunteerFirstName}!  Did you know our app rewards people who snap drives up quickly? There''s a new one-way drive from {vagueTo} to {clinic} on {timeDate}, so apply now and get rewarded!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (21, 1, N'We really need people to put on their hero capes and get these patients to their appointments!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (22, 1, N'It''s getting late and our callers are worried they won''t be able to have an abortion without your help. Open the app, sign up, and help ease their mind! They can''t do it without your help!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (23, 1, N'We still have shifts that need filling tomorrow! There''s over 80 of us in here, so I know we can get them all covered! Check the app and see if you can help out!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (24, 1, N'Did y''all know that every time a Sling shift gets filled, a unicorn gets its wings? We need more Pegasuses (Pegasi?)!! Open the app and get your wings today!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (25, 1, N'Our callers CANNOT reschedule and must have a ride tomorrow or they won''t be able to have an abortion. Please be their hero and apply in the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (26, 2, N'Hi {volunteerFirstName}, we''ve got a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. If you can help, sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (27, 2, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can you take a one-way drive to {vagueFrom} from {clinic} on {timeDate} sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (28, 2, N'We''ve got a one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app and be a hero!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (29, 2, N'Hey {volunteerFirstName}, there''s a brand new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Can you get them there? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (30, 2, N'Happy {dayOfTheWeek}! Can you take a one-way drive to {vagueFrom} from {clinic} on {timeDate}? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (31, 2, N'Hi {volunteerFirstName}, there''s a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app to make this caller''s day!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (32, 2, N'There''s a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app to get it while it''s hot!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (33, 2, N'Hi, {volunteerFirstName}! There''s a new one-way drive from {vagueFrom} to {clinic} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (34, 2, N'Hi, {volunteerFirstName}! I''m a cute new one-way drive from {vagueFrom} to {clinic} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (35, 2, N'Hi, {volunteerFirstName}! There''s a new one-way drive to {clinic} from {vagueFrom} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (36, 2, N'Hi, {volunteerFirstName}!  Did you know our app rewards people who snap drives up quickly? There''s a new one-way drive to {clinic} from {vagueFrom} on {timeDate}, so apply now and get rewarded!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (37, 4, N'Hi {volunteerFirstName}, we''ve got a new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app if you can help!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (38, 4, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (39, 4, N'We''ve got a round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app and be a hero!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (40, 4, N'Hey {volunteerFirstName}, there''s a new round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Can you get them there? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (41, 4, N'Happy {dayOfTheWeek}! Can you take a round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff?', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (42, 4, N'There''s a new  new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff}. Sign into the app to get it while it''s hot!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (43, 4, N'Hi {volunteerFirstName}! There''s a new round trip drive from {vagueLocation} to {clinic} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (44, 4, N'Hi, {volunteerFirstName}! I''m a cute new round trip drive from {vagueLocation} to {clinic} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (45, 4, N'Hi, {volunteerFirstName}! There''s a new round trip drive to {clinic} from {vagueLocation} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (46, 4, N'Hi, {volunteerFirstName}!  Did you know our app rewards people who snap drives up quickly? There''s a new round trip drive to {clinic} from {vagueLocation} on {timeDate}, so apply now and get rewarded!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (47, 4, N'Hi {volunteerFirstName}, there''s a new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff}. Sign into the app to make this caller''s day!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (48, 3, N'Hi {volunteerFirstName}, we''ve got a new round-trip drive from {vagueTo} to {clinic} on {timeDate}. If you can help, sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (49, 3, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a a round-trip drive from {vagueTo} to {clinic} on {timeDate} sign into the app!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (50, 3, N'We''ve got a round-trip drive from {vagueTo} to {clinic} on {timeDate}. Sign into the app and be a hero!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (51, 3, N'Hey {volunteerFirstName}, there''s a brand new round-trip drive from {vagueTo} to {clinic} on {timeDate}. Can you get them there? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (52, 3, N'Happy {dayOfTheWeek}! Can you take a round-trip drive from {vagueTo} to {clinic} on {timeDate}? ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (53, 3, N'Hi {volunteerFirstName}, there''s a  new round-trip drive to {vagueTo} from {clinic} on {timeDate}. Sign into the app to make this caller''s day!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (54, 3, N'There''s a new round-trip drive to {vagueTo} from {clinic} on {timeDate}. Sign into the app to get it while it''s hot!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (55, 3, N'Hi, {volunteerFirstName}! There''s a new round trip drive to {clinic} from {vagueTo} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (56, 3, N'Hi, {volunteerFirstName}! I''m a cute new round trip drive to {clinic} from {vagueTo} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (57, 3, N'Hi, {volunteerFirstName}! There''s a new round trip drive from {vagueTo} to {clinic} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride.', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (58, 3, N'Hi, {volunteerFirstName}!  Did you know our app rewards people who snap drives up quickly? There''s a new round trip drive from {vagueTo} to {clinic} on {timeDate}, so apply now and get rewarded!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (60, 9, N'{volunteerFirstName} has applied for drive {driveId}. Open the CASN app to approve this drive. ', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (62, 10, N'Your caller cancelled their drive {driveId}, so no need to pick up the patient. Thank you so much for volunteering anyway!', 1, @utcNow, NULL)
    INSERT [dbo].[Message] ([Id], [MessageType], [MessageText], [IsActive], [Created], [Updated]) VALUES (64, 11, N'Congratulations! You have been approved for drive {driveId}. Thank you so much for volunteering.', 1, @utcNow, NULL)
    SET IDENTITY_INSERT [dbo].[Message] OFF
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH