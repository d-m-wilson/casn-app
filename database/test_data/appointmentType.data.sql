LOCK TABLES `appointmenttype` WRITE;
/*!40000 ALTER TABLE `appointmenttype` DISABLE KEYS */;

INSERT INTO `appointmenttype` (`id`, `name`, `title`, `description`, `estimatedDurationMinutes`, `isActive`, `created`, `updated`) VALUES 
(3,'surgical_appointment','Surgical Appointment','Appointment to undergo surgical procedure.',210,0x01,'2018-10-26 01:00:19',NULL),
(4,'ultrasound appointment','Ultrasound Appointment','Appointment to receive ultrasound in advance of surgical procedure.',150,0x01,'2018-10-26 01:00:26',NULL),
(5,'lam insert','Lam Insert',NULL,90,0x01,'2019-04-19 16:48:48',NULL),
(6,'lam to complete','Lam to Complete',NULL,180,0x01,'2019-04-19 16:48:48',NULL),
(7,'courthouse appointment','Courthouse Appointment',NULL,60,0x01,'2019-04-19 16:48:48',NULL),
(8,'follow up appointment','Follow-up Appointment',NULL,30,0x01,'2019-04-19 16:48:49',NULL);

/*!40000 ALTER TABLE `appointmenttype` ENABLE KEYS */;
UNLOCK TABLES;
