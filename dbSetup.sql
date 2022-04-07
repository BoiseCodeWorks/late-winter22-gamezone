CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS games(
  id INT AUTO_INCREMENT primary key,
  creatorId varchar(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL,
  description varchar(5000) DEFAULT 'no description provided',
  recommendedPlayers INT DEFAULT 1,
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';
DROP TABLE IF EXISTS games;
ALTER TABLE
  games
ADD
  COLUMN creatorId varchar(255) NOT NULL;
INSERT INTO
  games (name, description, recommendedPlayers, creatorId)
VALUES
  (
    'Monopoly',
    'Do not pass go',
    4,
    '60d3560eceb6bbdfae388576'
  );
SELECT
  *
FROM
  games;
SELECT
  g.*,
  a.*
FROM
  games g
  JOIN accounts a
WHERE
  a.id = g.creatorId;
DELETE FROM
  games
WHERE
  id = 18
LIMIT
  1;