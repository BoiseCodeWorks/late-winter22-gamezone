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
CREATE TABLE IF NOT EXISTS gamePlayers(
  id INT AUTO_INCREMENT primary key,
  gameId INT NOT NULL,
  accountId VARCHAR(255) NOT NULL,
  score INT DEFAULT 0,
  FOREIGN KEY (gameId) REFERENCES games(id) ON DELETE CASCADE,
  FOREIGN Key (accountId) REFERENCES accounts(id) ON DELETE CASCADE
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
INSERT INTO
  gameplayers (gameId, accountId, score)
VALUES
  (2, "6234ac00abca50735a3c9205", 50);
SELECT
  *
FROM
  gameplayers;
SELECT
  *
FROM
  gameplayers
WHERE
  gameId = 2;
SELECT
  a.name,
  a.picture,
  gp.score,
  gp.id AS gamePlayerId
FROM
  gameplayers gp
  JOIN accounts a ON gp.accountId = a.id
WHERE
  gp.gameId = 1;
-- GET MY ACCOUNT GAMES
SELECT
  a.name,
  gp.score,
  g.name
FROM
  gameplayers gp
  JOIN accounts a ON gp.accountId = a.id
  JOIN games g ON gp.gameId = g.id
WHERE
  gp.accountId = "6234ac00abca50735a3c9205";
-- GET GAME ACCOUNTS
SELECT
  a.name,
  gp.score,
  g.name
FROM
  gameplayers gp
  JOIN accounts a ON gp.accountId = a.id
  JOIN games g ON gp.gameId = g.id
WHERE
  gp.gameId = 1;