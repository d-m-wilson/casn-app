BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @utcNow DATETIME = GETUTCDATE()

    SET IDENTITY_INSERT [dbo].[Message] ON 

    INSERT [dbo].[Message] ([Id], [MessageTypeId], [MessageText], [IsActive], [Created], [Updated]) VALUES 
    (1, 1, 'We''ve got a one-way drive to {clinic} from {vagueTo} on {timeDate}. Can you help? {appUrl}', 'True', @utcNow, NULL),
    (2, 2, 'We''ve got a one-way drive to {vagueFrom} from {clinic} on {timeDate}. Can you help? {appUrl}', 'True', @utcNow, NULL),
    (3, 3, 'We''ve got a round-trip drive from {vagueTo} to {clinic} on {timeDate}. Can you help? {appUrl}', 'True', @utcNow, NULL),
    (4, 4, 'We''ve got a round-trip drive from {vagueTo} to {clinic} returning to {vaugeFrom} on {timeDate}. Can you help? {appUrl}', 'True', @utcNow, NULL),
    (5, 5, 'URGENT! We''ve got a caller that needs your help TODAY. Please sign into the app and be a hero! {appUrl}', 'True', @utcNow, NULL),
    (6, 6, 'Friends, Houstonians, countrymen, lend me your ears! The following rides still need to be filled for tomorrow. Let''s get them there! {appUrl}', 'True', @utcNow, NULL),
    (7, 7, 'It''s getting late, and these callers still need you. Can you sign up for any of these drives? {appUrl}', 'True', @utcNow, NULL),
    (8, 8, 'These folks need your help, and we are down to the wire. Please, please can you take one of these drives? {appUrl}', 'True', @utcNow, NULL),
    (9, 1, 'Hi {volunteerFirstName}, we''ve got a new one-way drive to {clinic} from {vagueTo} on {timeDate}. If you can help, sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (10, 1, 'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a one-way drive to {clinic} from {vagueTo} on {timeDate}, sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (11, 1, 'We''ve got a one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app and be a hero! {appUrl}', 'True', @utcNow, NULL),
    (12, 1, 'Hey {volunteerFirstName}, there''s a brand new one-way drive to {clinic} from {vagueTo} on {timeDate}. Can you get them there? {appUrl}', 'True', @utcNow, NULL),
    (13, 1, 'Happy {dayOfTheWeek}! Can you take a one-way drive to {clinic} from {vagueTo} on {timeDate}? {appUrl}', 'True', @utcNow, NULL),
    (14, 1, 'Hi {volunteerFirstName}, there''s a new one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app to make this caller''s day! {appUrl}', 'True', @utcNow, NULL),
    (15, 1, 'There''s a new one-way drive to {clinic} from {vagueTo} on {timeDate}. Sign into the app to get it while it''s hot! {appUrl}', 'True', @utcNow, NULL),
    (16, 1, 'Hi, {volunteerFirstName}! There''s a new one-way drive to {clinic} from {vagueTo} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 'True', @utcNow, NULL),
    (17, 1, 'Hi, {volunteerFirstName}! I''m a cute new one-way drive to {clinic} from {vagueTo} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 'True', @utcNow, NULL),
    (18, 1, 'Hi, {volunteerFirstName}! There''s a new one-way drive from {vagueTo} to {clinic} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 'True', @utcNow, NULL),
    (19, 1, 'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new one-way drive from {vagueTo} to {clinic} on {timeDate}, so apply now and get rewarded! {appUrl}', 'True', @utcNow, NULL),
    (20, 7, 'We really need people to put on their hero capes and get these callers to their appointments! {appUrl}', 'True', @utcNow, NULL),
    (21, 8, 'It''s getting late and our callers are worried they won''t be able to have an abortion without your help. Open the app, sign up, and help ease their mind! They can''t do it without your help! {appUrl}', 'True', @utcNow, NULL),
    (22, 7, 'We still have shifts that need filling tomorrow! There''s over 80 of us in here, so I know we can get them all covered! Check the app and see if you can help out! {appUrl}', 'True', @utcNow, NULL),
    (23, 6, 'Did y''all know that every time a shift gets filled, a unicorn gets its wings? We need more Pegasuses (Pegasi?)!! Open the app and get your wings today! {appUrl}', 'True', @utcNow, NULL),
    (24, 8, 'Our callers CANNOT reschedule and must have a ride tomorrow or they won''t be able to have an abortion. Please be their hero and apply in the app! {appUrl}', 'True', @utcNow, NULL),
    (25, 2, 'Hi {volunteerFirstName}, we''ve got a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. If you can help, sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (26, 2, 'Hi {volunteerFirstName}, a CASN caller needs your help! If you can you take a one-way drive to {vagueFrom} from {clinic} on {timeDate} sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (27, 2, 'We''ve got a one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app and be a hero! {appUrl}', 'True', @utcNow, NULL),
    (28, 2, 'Hey {volunteerFirstName}, there''s a brand new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Can you get them there? {appUrl}', 'True', @utcNow, NULL),
    (29, 2, 'Happy {dayOfTheWeek}! Can you take a one-way drive to {vagueFrom} from {clinic} on {timeDate}? {appUrl}', 'True', @utcNow, NULL),
    (30, 2, 'Hi {volunteerFirstName}, there''s a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app to make this caller''s day! {appUrl}', 'True', @utcNow, NULL),
    (31, 2, 'There''s a new one-way drive to {vagueFrom} from {clinic} on {timeDate}. Sign into the app to get it while it''s hot! {appUrl}', 'True', @utcNow, NULL),
    (32, 2, 'Hi, {volunteerFirstName}! There''s a new one-way drive from {vagueFrom} to {clinic} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 'True', @utcNow, NULL),
    (33, 2, 'Hi, {volunteerFirstName}! I''m a cute new one-way drive from {vagueFrom} to {clinic} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 'True', @utcNow, NULL),
    (34, 2, 'Hi, {volunteerFirstName}! There''s a new one-way drive to {clinic} from {vagueFrom} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 'True', @utcNow, NULL),
    (35, 2, 'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new one-way drive to {clinic} from {vagueFrom} on {timeDate}, so apply now and get rewarded! {appUrl}', 'True', @utcNow, NULL),
    (36, 4, 'Hi {volunteerFirstName}, we''ve got a new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app if you can help! {appUrl}', 'True', @utcNow, NULL),
    (37, 4, 'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (38, 4, 'We''ve got a round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app and be a hero! {appUrl}', 'True', @utcNow, NULL),
    (39, 4, 'Hey {volunteerFirstName}, there''s a new round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Can you get them there? {appUrl}', 'True', @utcNow, NULL),
    (40, 4, 'Happy {dayOfTheWeek}! Can you take a round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff? {appUrl}', 'True', @utcNow, NULL),
    (41, 4, 'There''s a new new round-trip drive on on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff}. Sign into the app to get it while it''s hot! {appUrl}', 'True', @utcNow, NULL),
    (42, 4, 'Hi {volunteerFirstName}! There''s a new round trip drive from {vagueFrom} to {clinic} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 'True', @utcNow, NULL),
    (43, 4, 'Hi, {volunteerFirstName}! I''m a cute new round trip drive from {vagueFrom} to {clinic} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 'True', @utcNow, NULL),
    (44, 4, 'Hi, {volunteerFirstName}! There''s a new round trip drive to {clinic} from {vagueFrom} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 'True', @utcNow, NULL),
    (45, 4, 'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new round trip drive to {clinic} from {vagueFrom} on {timeDate}, so apply now and get rewarded! {appUrl}', 'True', @utcNow, NULL),
    (46, 4, 'Hi {volunteerFirstName}, there''s a new round-trip drive on {timeDate} to {clinic}, with a {vagueFrom} pickup and a {vagueTo} dropoff. Sign into the app to make this caller''s day! {appUrl}', 'True', @utcNow, NULL),
    (47, 3, 'Hi {volunteerFirstName}, we''ve got a new round-trip drive from {vagueTo} to {clinic} on {timeDate}. If you can help, sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (48, 3, 'Hi {volunteerFirstName}, a CASN caller needs your help! If you can take a a round-trip drive from {vagueTo} to {clinic} on {timeDate} sign into the app! {appUrl}', 'True', @utcNow, NULL),
    (49, 3, 'We''ve got a round-trip drive from {vagueTo} to {clinic} on {timeDate}. Sign into the app and be a hero! {appUrl}', 'True', @utcNow, NULL),
    (50, 3, 'Hey {volunteerFirstName}, there''s a brand new round-trip drive from {vagueTo} to {clinic} on {timeDate}. Can you get them there? {appUrl}', 'True', @utcNow, NULL),
    (51, 3, 'Happy {dayOfTheWeek}! Can you take a round-trip drive from {vagueTo} to {clinic} on {timeDate}? {appUrl}', 'True', @utcNow, NULL),
    (52, 3, 'Hi {volunteerFirstName}, there''s a new round-trip drive to {vagueTo} from {clinic} on {timeDate}. Sign into the app to make this caller''s day! {appUrl}', 'True', @utcNow, NULL),
    (53, 3, 'There''s a new round-trip drive to {vagueTo} from {clinic} on {timeDate}. Sign into the app to get it while it''s hot! {appUrl}', 'True', @utcNow, NULL),
    (54, 3, 'Hi, {volunteerFirstName}! There''s a new round trip drive to {clinic} from {vagueTo} on {timeDate} that needs an abortion access hero! If that''s you, sign into the app and let''s get her there! {appUrl}', 'True', @utcNow, NULL),
    (55, 3, 'Hi, {volunteerFirstName}! I''m a cute new round trip drive to {clinic} from {vagueTo} on {timeDate} that needs filling! Open the app and swipe right if you''re the one for me! {appUrl}', 'True', @utcNow, NULL),
    (56, 3, 'Hi, {volunteerFirstName}! There''s a new round trip drive from {vagueTo} to {clinic} on {timeDate} that needs an abortion access hero! Our callers are always so scared they won''t get a ride. {appUrl}', 'True', @utcNow, NULL),
    (57, 3, 'Hi, {volunteerFirstName}! Did you know our app rewards people who snap drives up quickly? There''s a new round trip drive from {vagueTo} to {clinic} on {timeDate}, so apply now and get rewarded! {appUrl}', 'True', @utcNow, NULL),
    (58, 9, '{volunteerFirstName} has applied for a drive for caller {callerIdentifier} on {timeDate}. Open the CASN app to approve this drive. {appUrl}', 'True', @utcNow, NULL),
    (59, 10, 'Your caller ({callerIdentifier}) cancelled their drive, so no need to pick them up. Thank you so much for volunteering anyway!', 'True', @utcNow, NULL),
    (60, 11, 'Congratulations! You have been approved to drive caller {callerIdentifier} on {timeDate}. Thank you so much for volunteering. {appUrl}', 'True', @utcNow, NULL),
    (61, 6, 'Hey there, heroes! There are still drives left open for tomorrow that need to be filled. Can you help us out with any of these? {appUrl}', 'True', @utcNow, NULL),
    (62, 6, 'Hi friends! There are still drives left open that need to be filled. Don''t you want to be someone''s hero tomorrow? {appUrl}', 'True', @utcNow, NULL),
    (63, 6, 'There are still drives left open for tomorrow - sign into the app now to pick yours! {appUrl}', 'True', @utcNow, NULL),
    (64, 6, 'Good evening, abortion access heroes! There are still drives left for tomorrow! Sign into the app if you can help! {appUrl}', 'True', @utcNow, NULL),
    (65, 6, 'We''re still looking for abortion access heroes for tomorrow. Please sign into the app to get them there! {appUrl}', 'True', @utcNow, NULL),
    (66, 7, 'Help our callers exercise their constitutional rights. Tap the link and be a hero today! {appUrl}', 'True', @utcNow, NULL),
    (67, 7, 'Help our callers recieve the healthcare they deserve. Tap the link and be a hero today! {appUrl}', 'True', @utcNow, NULL),
    (68, 7, 'Our callers are getting anxious about tomorrow - tap the link to help them ease their worries! {appUrl}', 'True', @utcNow, NULL),
    (69, 7, 'Time is running out to get tomorrow''s callers the help they need. Tap the link to get them there! {appUrl}', 'True', @utcNow, NULL),
    (70, 8, 'If you''ve been waiting to sign up for a drive, now is the time. Tap the link and be a hero today! {appUrl}', 'True', @utcNow, NULL),
    (71, 8, 'This is your last chance to help someone who desperately needs access to abortion care tomorrow. Tap the link and don''t let them down! {appUrl}', 'True', @utcNow, NULL),
    (72, 8, 'We''re here because we believe in helping people access the care they need. This is your last chance to be someone''s hero tomorrow! {appUrl}', 'True', @utcNow, NULL)

    SET IDENTITY_INSERT [dbo].[Message] OFF

    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH
