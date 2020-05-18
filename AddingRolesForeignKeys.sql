-- Adding Foreign KeyS 
ALTER TABLE _UserRolesMapping
ADD FOREIGN KEY (UserID) REFERENCES _Users(UserID);
ALTER TABLE _UserRolesMapping
ADD FOREIGN KEY (RoleID) REFERENCES _UserRoles(ID);