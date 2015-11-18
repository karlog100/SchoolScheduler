#Create student example
INSERT INTO `users` (`name`, `address`, `postcode`, `phone`, `email`)
    VALUES('student1', 'student street 1', '1111', '11111111', 'student1@school.dk');
INSERT INTO `student` (`user_id`,`education_start_date`, `education_end_date`)
    VALUES(LAST_INSERT_ID(), '2015-01-01', '2015-12-24');

#Select student based on email:
SELECT `users`.`id`, `users`.`name`, `users`.`address`, `users`.`postcode`, `users`.`phone`, `users`.`email`, `student`.`education_start_date`, `student`.`education_end_date` FROM `school_scheduler`.`users` INNER JOIN `school_scheduler`.`student` ON `users`.`id` = `student`.`user_id` WHERE `users`.`email` = 'student1@school.dk' LIMIT 1;

#Update student in one statement
UPDATE `users` JOIN `student` ON `users`.`id` = `student`.`user_id` SET `users`.`name` = 'student42' WHERE `users`.`id` = 1;

#If exsits then update, else create student   (note not sure if last_insert_id works when updateing, will debug later)
INSERT INTO `users` (`id`, `name`, `address`, `postcode`, `phone`, `email`)
    VALUES(NULL,'student3', 'student street 1', '1111', '11111111', 'student3@school.dk')
    ON DUPLICATE KEY UPDATE `name` = 'student3', `address` = 'student street 3', `postcode` = '6440', `phone` = '465456', `email` = 'student3@school.dk';
INSERT INTO `student` (`user_id`,`education_start_date`, `education_end_date`)
    VALUES(LAST_INSERT_ID(), '2015-01-01', '2015-12-24')
    ON DUPLICATE KEY UPDATE `education_start_date` = '2015-01-02', `education_end_date` = '2015-12-25';
    
 #Delete a user  (if you delete a user it wil also delete the user from, class, group´s, student/teacher etc with the current setup)
 DELETE FROM `school_scheduler`.`users` WHERE `id` = 4;
 
 SELECT * FROM `class`;
 
 #Create class
 INSERT INTO `school_scheduler`.`class` (`name`, `start_datetime`, `end_datetime`)
	VALUES('someClass1', '2015-08-08', '2015-08-09');
    

SELECT * FROM `group`;
#Create group --> group is a list of useres it can be ether students or teatcers or a mix, is a user gets removed from a grup it will also get removed from all the classes that the group is attening
INSERT INTO `school_scheduler`.`group` (`name`)
	VALUES('testGroup2');
    
SELECT * FROM `school_scheduler`.`group_list`;
#Insert useres to a group (users can be in multiple groups, but only once in each group)
INSERT INTO `school_scheduler`.`group_list` (`group_id`, `user_id`)
	VALUES(1, 2);

#Select useres in x group    
SELECT * FROM `school_scheduler`.`users` 
	INNER JOIN `school_scheduler`.`group_list` ON `users`.`id` = `group_list`.`user_id`
    INNER JOIN `school_scheduler`.`group` ON `group`.`id` = `group_list`.`group_id`
    WHERE `group`.`id` = 1;
    
SELECT * FROM `class`;
SELECT * FROM `class_group_list`;
#Add group to class
INSERT INTO `school_scheduler`.`class_group_list` (`class_id`, `group_id`)
	VALUES(1, 1);

SELECT * FROM users;
#Add user to class
INSERT INTO `school_scheduler`.`class_user_list` (`class_id`, `user_id`)
	VALUES(1, 5);

SELECT * FROM `class`;
#Select all useres that are linked to x class
SELECT `users`.`id`, `users`.`name` FROM `school_scheduler`.`users`
	WHERE 
    `users`.`id` IN (SELECT `user_id` FROM `school_scheduler`.`group_list` WHERE `group_list`.`group_id` 
			IN (SELECT `group_id` FROM `school_scheduler`.`class_group_list` WHERE `class_group_list`.`class_id` = 1))
     OR `users`.`id` IN (SELECT `user_id` FROM `school_scheduler`.`class_user_list` WHERE `class_user_list`.`class_id` = 1);
     
#Select all STUDENTS that are linked to x class (replace student with teacher for getting teatchers instead)
SELECT `users`.`id`, `users`.`name` FROM `school_scheduler`.`users`
	WHERE 
    (`users`.`id` IN (SELECT `user_id` FROM `school_scheduler`.`group_list` WHERE `group_list`.`group_id` 
			IN (SELECT `group_id` FROM `school_scheduler`.`class_group_list` WHERE `class_group_list`.`class_id` = 1))
     OR `users`.`id` IN (SELECT `user_id` FROM `school_scheduler`.`class_user_list` WHERE `class_user_list`.`class_id` = 1))
     AND `users`.`id` IN (SELECT `user_id` FROM `school_scheduler`.`student`);
     
     
#Select all classes that x user is linked to based on user id
SELECT `class`.`id`, `class`.`name` FROM `school_scheduler`.`class`
	WHERE
		`class`.`id` IN (SELECT `class_id` FROM `school_scheduler`.`class_user_list` WHERE `class_user_list`.`user_id` = 1)
        OR `class`.`id` IN (SELECT `class_id` FROM `school_scheduler`.`class_group_list` WHERE `class_group_list`.`group_id` 
				IN (SELECT `group_id` FROM `school_scheduler`.`group_list` WHERE `user_id` = 1));
                
#Select a list of groups a user is in
SELECT `group`.`id`, `group`.`name` FROM `school_scheduler`.`group` 
	WHERE `group`.`id` IN (SELECT `group_list`.`group_id` FROM `school_scheduler`.`group_list` WHERE `group_list`.`user_id` = 1);

