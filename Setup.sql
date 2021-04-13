USE jobsapi;


-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );

-- CREATE TABLE jobs 
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     name VARCHAR(255) NOT NULL,
--     location VARCHAR(255) NOT NULL,
--     budget INT NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE 
-- )


-- CREATE TABLE contractors
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     name VARCHAR(255) NOT NULL,
--     age INT NOT NULL,
--     salary INT NOT NULL,
--     creatorId VARCHAR(255),

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- )


CREATE TABLE jobcontractors 
(
    id INT NOT NULL AUTO_INCREMENT,
    jobId INT NOT NULL,
    contractorId INT NOT NULL,
    creatorId VARCHAR(255),

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE,

    FOREIGN KEY (jobId)
    REFERENCES jobs (id)
    ON DELETE CASCADE,

    FOREIGN KEY (contractorId)
    REFERENCES contractors (id)
    ON DELETE CASCADE
)