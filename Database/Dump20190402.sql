-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: localhost    Database: troby
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `achievements`
--

DROP TABLE IF EXISTS `achievements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `achievements` (
  `id` int(11) NOT NULL,
  `title` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `worth` enum('bronze','silver','gold','platinum') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `score` int(4) NOT NULL,
  `description` tinytext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `gameId` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_achievements_games` (`gameId`),
  CONSTRAINT `FK_achievements_games` FOREIGN KEY (`gameId`) REFERENCES `games` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `achievements`
--

LOCK TABLES `achievements` WRITE;
/*!40000 ALTER TABLE `achievements` DISABLE KEYS */;
INSERT INTO `achievements` VALUES (800,'Get shoed!','bronze',100,'Win as the shoe',200),(801,'Bark! Bark!','bronze',100,'Win as the dog',200);
/*!40000 ALTER TABLE `achievements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `games`
--

DROP TABLE IF EXISTS `games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `games` (
  `id` int(11) NOT NULL,
  `name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `bgg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `year` int(4) DEFAULT NULL,
  `image` text COLLATE utf8mb4_swedish_ci,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `games`
--

LOCK TABLES `games` WRITE;
/*!40000 ALTER TABLE `games` DISABLE KEYS */;
INSERT INTO `games` VALUES (200,'Monopoly','https://boardgamegeek.com/boardgame/1406/monopoly','the classic property trading game!',1933,'https://cf.geekdo-images.com/imagepagezoom/img/jUy1SJOgYxnAEV0BNVpTpEn-On8=/fit-in/1200x900/filters:no_upscale()/pic4235383.jpg'),(203,'Eldritch Horror','https://boardgamegeek.com/boardgame/146021/eldritch-horror','The end draws near! Do you have the courage to prevent global destruction?',2013,'https://cf.geekdo-images.com/imagepage/img/vi6Xlm-4sSXQHkamQB7Rcs3dHF8=/fit-in/900x600/filters:no_upscale()/pic1872452.jpg'),(205,'Gloomhaven','https://boardgamegeek.com/boardgame/174430/gloomhaven','Gloomhaven is a game of Euro-inspired tactical combat in a persistent world of shifting motives. Players will take on the role of a wandering adventurer with their own special set of skills and their own reasons for traveling to this dark corner of the world.',2017,'https://cf.geekdo-images.com/imagepagezoom/img/gmW4WxIHPcffwAS-L2LTg6cFiLo=/fit-in/1200x900/filters:no_upscale()/pic2437871.jpg');
/*!40000 ALTER TABLE `games` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ownership`
--

DROP TABLE IF EXISTS `ownership`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ownership` (
  `gameId` int(11) NOT NULL,
  `userEmail` varchar(255) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL,
  `id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `asd_idx` (`userEmail`),
  CONSTRAINT `FK_ownership_users` FOREIGN KEY (`userEmail`) REFERENCES `users` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ownership`
--

LOCK TABLES `ownership` WRITE;
/*!40000 ALTER TABLE `ownership` DISABLE KEYS */;
INSERT INTO `ownership` VALUES (200,'asd@asd.se',1),(205,'asd@asd.se',2),(205,'asd@asd.se',21);
/*!40000 ALTER TABLE `ownership` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unlocks`
--

DROP TABLE IF EXISTS `unlocks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `unlocks` (
  `id` int(11) NOT NULL,
  `date` date NOT NULL,
  `achId` int(11) NOT NULL,
  `userId` varchar(255) CHARACTER SET utf8 COLLATE utf8_swedish_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_unlocks_users_idx` (`userId`),
  KEY `FK_unlocks_achievements` (`achId`),
  CONSTRAINT `FK_unlocks_achievements` FOREIGN KEY (`achId`) REFERENCES `achievements` (`id`),
  CONSTRAINT `FK_unlocks_users` FOREIGN KEY (`userId`) REFERENCES `users` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unlocks`
--

LOCK TABLES `unlocks` WRITE;
/*!40000 ALTER TABLE `unlocks` DISABLE KEYS */;
INSERT INTO `unlocks` VALUES (500,'2019-04-02',800,'asd@asd.se');
/*!40000 ALTER TABLE `unlocks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `users` (
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL,
  `hash` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `salt` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_swedish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('asd@asd.se','LEqnOovEhO4J60yVud27lfXEBXArz/ciMXOtCUddTXg=','POm10/8eMUz9qwWPe0gsAwjefxiqd96B1z2cKNLVx64='),('kalle@asd.se','2lMIurQgv237OvHXr18EIJKB2cbdmbOuUjRAq9E02+8=','XizLwEd5yVL7aZysyNb1ukI1I9ege0voKD3y907Sg44='),('olle@asd.se','s36i2HOo3vKkj3igads9vB8fGpDfp9Tw31IKO7JKzr4=','u/ZOhnuJROvtVK5hLLIBCVjHJgPxYpPeA086c1WaNdM='),('qwe@asd.se','35+h9xW2UU+R17rqGdnAJJ0ih+0NfL6fWvUZaJskgS8=','IHfZTjH4cBxHG73W17wKcvuCDeZzljTCVrtEFC5FZx0=');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-02 21:37:24
