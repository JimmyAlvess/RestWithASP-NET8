﻿CREATE TABLE dbo.Books(
id INT PRIMARY KEY IDENTITY(1,1),
title NVARCHAR(100),
author NVARCHAR(100),
price DECIMAL(10,2),
launch_date DATETIME
);
