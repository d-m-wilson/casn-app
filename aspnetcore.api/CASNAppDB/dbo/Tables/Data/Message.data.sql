BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()

    SET IDENTITY_INSERT [dbo].[Message] ON 

    INSERT [dbo].[Message] ([Id], [MessageTypeId], [MessageText], [IsActive], [Created], [Updated]) VALUES 
    (1, 1, N'We''ve got a one-way drive to {clinic} from {vagueTo} on {timeDate}. Can you help? {appUrl}', 1, @utcNow, NULL),
    (2, 2, N'We''ve got a one-way drive to {vagueFrom} from {clinic} on {timeDate}. Can you help? {appUrl}', 1, @utcNow, NULL),
    (3, 3, N'We''ve got a round-trip drive from {vagueTo} to {clinic} on {timeDate}. Can you help? {appUrl}', 1, @utcNow, NULL),
    (4, 4, N'We''ve got a round-trip drive from {vagueTo} to {clinic} returning to {vaugeFrom} on {timeDate}. Can you help? {appUrl}', 1, @utcNow, NULL),
    (5, 5, N'URGENT! We''ve got a caller that needs your help TODAY. Please sign into the app and be a hero! {appUrl}', 1, @utcNow, NULL),
    (6, 6, N'Friends, Houstonians, countrymen, lend me your ears! The following rides still need to be filled for tomorrow. Let''s get them there! {appUrl}', 1, @utcNow, NULL),
    (7, 7, N'It''s getting late, and these callers still need you. Can you sign up for any of these drives? {appUrl}', 1, @utcNow, NULL),
    (8, 8, N'These folks need your help, and we are down to the wire. Please, please can you take one of these drives? {appUrl}', 1, @utcNow, NULL),
    (9, 1, N'Hi {volunteerFirstName}, we''ve got a new one-way drive to {clinic} from {vagueTo} on {timeDate}. If you can help, sign into the app! {appUrl}', 1, @utcNow, NULL),
    (10, 1, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a one-way drive to {clinic} from {vagueTo} on {timeDate}, sign into the app! {appUrl}', 1, @utcNow, NULL),
    (11, 1, N'We''ve got a one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app and be a hero! {appUrl}', 1, @utcNow, NULL),
    (12, 1, N'Hey {volunteerFirstName}, there''s a brand new one-way drive to {clinic} from {vagueTo} on {timeDate}. Can you get them there? {appUrl}', 1, @utcNow, NULL),
    (13, 1, N'Happy {dayOfTheWeek}! Can you take a one-way drive to {clinic} from {vagueTo} on {timeDate}? {appUrl}', 1, @utcNow, NULL),
    (14, 1, N'Hi {volunteerFirstName}, there''s a new one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app to make this caller''s day! {appUrl}', 1, @utcNow, NULL),
    (15, 1, N'There''s a new one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app to get it while it''s hot! {appUrl}', 1, @utcNow, NULL),
    (16, 1, N'Hi, {volunteerFirstName}! There''s a new one-way drive to {clinic} from {vagueTo} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 1, @utcNow, NULL),
    (17, 1, N'Hi, {volunteerFirstName}! I''m a cute new one-way drive to {clinic} from {vagueTo} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 1, @utcNow, NULL),
    (18, 1, N'Hi, {volunteerFirstName}! There''s a new one-way drive from {vagueTo} to {clinic} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 1, @utcNow, NULL),
    (19, 1, N'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new one-way drive from {vagueTo} to {clinic} on {timeDate}, so apply now and get rewarded! {appUrl}', 1, @utcNow, NULL),
    (20, 7, N'We really need people to put on their hero capes and get these callers to their appointments! {appUrl}', 1, @utcNow, NULL),
    (21, 8, N'It''s getting late and our callers are worried they won''t be able to have an abortion without your help. Open the app, sign up, and help ease their mind! They can''t do it without your help! {appUrl}', 1, @utcNow, NULL),
    (22, 7, N'We still have shifts that need filling tomorrow! There''s over 80 of us in here, so I know we can get them all covered! Check the app and see if you can help out! {appUrl}', 1, @utcNow, NULL),
    (23, 6, N'Did y''all know that every time a shift gets filled, a unicorn gets its wings? We need more Pegasuses (Pegasi?)!! Open the app and get your wings today! {appUrl}', 1, @utcNow, NULL),
    (24, 8, N'Our callers CANNOT reschedule and must have a ride tomorrow or they won''t be able to have an abortion. Please be their hero and apply in the app! {appUrl}', 1, @utcNow, NULL),
    (25, 2, N'Hi {volunteerFirstName}, we''ve got a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. If you can help, sign into the app! {appUrl}', 1, @utcNow, NULL),
    (26, 2, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can you take a one-way drive to {vagueFrom} from {clinic} on {timeDate} sign into the app! {appUrl}', 1, @utcNow, NULL),
    (27, 2, N'We''ve got a one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app and be a hero! {appUrl}', 1, @utcNow, NULL),
    (28, 2, N'Hey {volunteerFirstName}, there''s a brand new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Can you get them there? {appUrl}', 1, @utcNow, NULL),
    (29, 2, N'Happy {dayOfTheWeek}! Can you take a one-way drive to {vagueFrom} from {clinic} on {timeDate}? {appUrl}', 1, @utcNow, NULL),
    (30, 2, N'Hi {volunteerFirstName}, there''s a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app to make this caller''s day! {appUrl}', 1, @utcNow, NULL),
    (31, 2, N'There''s a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app to get it while it''s hot! {appUrl}', 1, @utcNow, NULL),
    (32, 2, N'Hi, {volunteerFirstName}! There''s a new one-way drive from {vagueFrom} to {clinic} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 1, @utcNow, NULL),
    (33, 2, N'Hi, {volunteerFirstName}! I''m a cute new one-way drive from {vagueFrom} to {clinic} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 1, @utcNow, NULL),
    (34, 2, N'Hi, {volunteerFirstName}! There''s a new one-way drive to {clinic} from {vagueFrom} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 1, @utcNow, NULL),
    (35, 2, N'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new one-way drive to {clinic} from {vagueFrom} on {timeDate}, so apply now and get rewarded! {appUrl}', 1, @utcNow, NULL),
    (36, 4, N'Hi {volunteerFirstName}, we''ve got a new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app if you can help! {appUrl}', 1, @utcNow, NULL),
    (37, 4, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff sign into the app! {appUrl}', 1, @utcNow, NULL),
    (38, 4, N'We''ve got a round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app and be a hero! {appUrl}', 1, @utcNow, NULL),
    (39, 4, N'Hey {volunteerFirstName}, there''s a new round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Can you get them there? {appUrl}', 1, @utcNow, NULL),
    (40, 4, N'Happy {dayOfTheWeek}! Can you take a round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff? {appUrl}', 1, @utcNow, NULL),
    (41, 4, N'There''s a new new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff}. Sign into the app to get it while it''s hot! {appUrl}', 1, @utcNow, NULL),
    (42, 4, N'Hi {volunteerFirstName}! There''s a new round trip drive from {vagueFrom} to {clinic} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 1, @utcNow, NULL),
    (43, 4, N'Hi, {volunteerFirstName}! I''m a cute new round trip drive from {vagueFrom} to {clinic} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 1, @utcNow, NULL),
    (44, 4, N'Hi, {volunteerFirstName}! There''s a new round trip drive to {clinic} from {vagueFrom} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 1, @utcNow, NULL),
    (45, 4, N'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new round trip drive to {clinic} from {vagueFrom} on {timeDate}, so apply now and get rewarded! {appUrl}', 1, @utcNow, NULL),
    (46, 4, N'Hi {volunteerFirstName}, there''s a new round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app to make this caller''s day! {appUrl}', 1, @utcNow, NULL),
    (47, 3, N'Hi {volunteerFirstName}, we''ve got a new round-trip drive from {vagueTo} to {clinic} on {timeDate}. If you can help, sign into the app! {appUrl}', 1, @utcNow, NULL),
    (48, 3, N'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a a round-trip drive from {vagueTo} to {clinic} on {timeDate} sign into the app! {appUrl}', 1, @utcNow, NULL),
    (49, 3, N'We''ve got a round-trip drive from {vagueTo} to {clinic} on {timeDate}. Sign into the app and be a hero! {appUrl}', 1, @utcNow, NULL),
    (50, 3, N'Hey {volunteerFirstName}, there''s a brand new round-trip drive from {vagueTo} to {clinic} on {timeDate}. Can you get them there? {appUrl}', 1, @utcNow, NULL),
    (51, 3, N'Happy {dayOfTheWeek}! Can you take a round-trip drive from {vagueTo} to {clinic} on {timeDate}? {appUrl}', 1, @utcNow, NULL),
    (52, 3, N'Hi {volunteerFirstName}, there''s a new round-trip drive to {vagueTo} from {clinic} on {timeDate}. Sign into the app to make this caller''s day! {appUrl}', 1, @utcNow, NULL),
    (53, 3, N'There''s a new round-trip drive to {vagueTo} from {clinic} on {timeDate}. Sign into the app to get it while it''s hot! {appUrl}', 1, @utcNow, NULL),
    (54, 3, N'Hi, {volunteerFirstName}! There''s a new round trip drive to {clinic} from {vagueTo} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 1, @utcNow, NULL),
    (55, 3, N'Hi, {volunteerFirstName}! I''m a cute new round trip drive to {clinic} from {vagueTo} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 1, @utcNow, NULL),
    (56, 3, N'Hi, {volunteerFirstName}! There''s a new round trip drive from {vagueTo} to {clinic} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 1, @utcNow, NULL),
    (57, 3, N'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new round trip drive from {vagueTo} to {clinic} on {timeDate}, so apply now and get rewarded! {appUrl}', 1, @utcNow, NULL),
    (58, 9, N'{volunteerFirstName} has applied for a drive for caller {callerIdentifier} on {timeDate}. Open the CASN app to approve this drive. {appUrl}', 1, @utcNow, NULL),
    (59, 10, N'Your caller ({callerIdentifier}) cancelled their drive, so no need to pick them up. Thank you so much for volunteering anyway!', 1, @utcNow, NULL),
    (60, 11, N'Congratulations! You have been approved to drive caller {callerIdentifier} on {timeDate}. Thank you so much for volunteering. {appUrl}', 1, @utcNow, NULL),
    (61, 6, N'Hey there, heroes! There are still drives left open for tomorrow that need to be filled. Can you help us out with any of these? {appUrl}', 1, @utcNow, NULL),
    (62, 6, N'Hi friends! There are still drives left open that need to be filled. Don''t you want to be someone''s hero tomorrow? {appUrl}', 1, @utcNow, NULL),
    (63, 6, N'There are still drives left open for tomorrow - sign into the app now to pick yours! {appUrl}', 1, @utcNow, NULL),
    (64, 6, N'Good evening, abortion access heroes! There are still drives left for tomorrow! Sign into the app if you can help! {appUrl}', 1, @utcNow, NULL),
    (65, 6, N'We''re still looking for abortion access heroes for tomorrow. Please sign into the app to get them there! {appUrl}', 1, @utcNow, NULL),
    (66, 7, N'Help our callers exercise their constitutional rights. Tap the link and be a hero today! {appUrl}', 1, @utcNow, NULL),
    (67, 7, N'Help our callers recieve the healthcare they deserve. Tap the link and be a hero today! {appUrl}', 1, @utcNow, NULL),
    (68, 7, N'Our callers are getting anxious about tomorrow - tap the link to help them ease their worries! {appUrl}', 1, @utcNow, NULL),
    (69, 7, N'Time is running out to get tomorrow''s callers the help they need. Tap the link to get them there! {appUrl}', 1, @utcNow, NULL),
    (70, 8, N'If you''ve been waiting to sign up for a drive, now is the time. Tap the link and be a hero today! {appUrl}', 1, @utcNow, NULL),
    (71, 8, N'This is your last chance to help someone who desperately needs access to abortion care tomorrow. Tap the link and don''t let them down! {appUrl}', 1, @utcNow, NULL),
    (72, 8, N'We''re here because we believe in helping people access the care they need. This is your last chance to be someone''s hero tomorrow! {appUrl}', 1, @utcNow, NULL),
    (73, 12, N'You earned the {badgeName} badge! {badgeMessage} Sign into {appUrl} and open "My Achievements" from the menu to see your badge now.', 1, @utcNow, NULL)

    SET IDENTITY_INSERT [dbo].[Message] OFF

    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
