-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 26, 2025 at 01:48 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `base26052025`
--

-- --------------------------------------------------------

--
-- Table structure for table `animal`
--

CREATE TABLE `animal` (
  `ID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Type` int(11) NOT NULL,
  `Birthday` date DEFAULT NULL,
  `Mother` int(11) DEFAULT NULL,
  `Father` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `animal`
--

INSERT INTO `animal` (`ID`, `Name`, `Type`, `Birthday`, `Mother`, `Father`) VALUES
(5, 'Sarabi', 1, '2010-03-01', NULL, NULL),
(6, 'Mufasa', 1, '2010-06-15', NULL, NULL),
(8, 'Marty', 2, '2015-06-14', NULL, NULL),
(11, 'Simba', 1, '2022-05-05', 5, 6),
(12, 'Stripe', 2, '2023-03-03', NULL, 8);

-- --------------------------------------------------------

--
-- Table structure for table `animaltype`
--

CREATE TABLE `animaltype` (
  `ID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `animaltype`
--

INSERT INTO `animaltype` (`ID`, `Name`) VALUES
(1, 'Lion'),
(2, 'Zebra');

-- --------------------------------------------------------

--
-- Table structure for table `animal_food`
--

CREATE TABLE `animal_food` (
  `AnimalID` int(11) NOT NULL,
  `FoodTypeID` int(11) NOT NULL,
  `AmountPerDay` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `animal_food`
--

INSERT INTO `animal_food` (`AnimalID`, `FoodTypeID`, `AmountPerDay`) VALUES
(5, 1, 5),
(6, 1, 5),
(8, 2, 6);

-- --------------------------------------------------------

--
-- Table structure for table `foodtype`
--

CREATE TABLE `foodtype` (
  `ID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `foodtype`
--

INSERT INTO `foodtype` (`ID`, `Name`) VALUES
(1, 'Meat'),
(2, 'Grass');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `animal`
--
ALTER TABLE `animal`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Type` (`Type`),
  ADD KEY `Mother` (`Mother`),
  ADD KEY `Father` (`Father`);

--
-- Indexes for table `animaltype`
--
ALTER TABLE `animaltype`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `animal_food`
--
ALTER TABLE `animal_food`
  ADD PRIMARY KEY (`AnimalID`,`FoodTypeID`),
  ADD KEY `FoodTypeID` (`FoodTypeID`);

--
-- Indexes for table `foodtype`
--
ALTER TABLE `foodtype`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `animal`
--
ALTER TABLE `animal`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `animaltype`
--
ALTER TABLE `animaltype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `foodtype`
--
ALTER TABLE `foodtype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `animal`
--
ALTER TABLE `animal`
  ADD CONSTRAINT `animal_ibfk_1` FOREIGN KEY (`Type`) REFERENCES `animaltype` (`ID`),
  ADD CONSTRAINT `animal_ibfk_2` FOREIGN KEY (`Mother`) REFERENCES `animal` (`ID`),
  ADD CONSTRAINT `animal_ibfk_3` FOREIGN KEY (`Father`) REFERENCES `animal` (`ID`);

--
-- Constraints for table `animal_food`
--
ALTER TABLE `animal_food`
  ADD CONSTRAINT `animal_food_ibfk_1` FOREIGN KEY (`AnimalID`) REFERENCES `animal` (`ID`),
  ADD CONSTRAINT `animal_food_ibfk_2` FOREIGN KEY (`FoodTypeID`) REFERENCES `foodtype` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
