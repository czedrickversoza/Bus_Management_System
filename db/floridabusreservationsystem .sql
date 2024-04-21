-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 14, 2024 at 02:27 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET FOREIGN_KEY_CHECKS=0;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `floridabusreservationsystem`
--
CREATE DATABASE IF NOT EXISTS `floridabusreservationsystem` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `floridabusreservationsystem`;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_bus`
--

CREATE TABLE `tbl_bus` (
  `bus_id` int(255) NOT NULL,
  `bus_name` varchar(255) NOT NULL,
  `bus_type` varchar(255) NOT NULL,
  `bus_max_seat` int(255) NOT NULL,
  `available_seat` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_bus`
--

INSERT INTO `tbl_bus` (`bus_id`, `bus_name`, `bus_type`, `bus_max_seat`, `available_seat`) VALUES
(1, 'Cc-ccc', 'Deluxe', 32, 31);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_passenger`
--

CREATE TABLE `tbl_passenger` (
  `passenger_id` int(255) NOT NULL,
  `passenger_name` varchar(255) NOT NULL,
  `mobile_number` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `address` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_passenger`
--

INSERT INTO `tbl_passenger` (`passenger_id`, `passenger_name`, `mobile_number`, `email`, `address`) VALUES
(1, 'czedrick versoza', '0980908043', 'sample@gmail.com', 'cagayan');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_reserves`
--

CREATE TABLE `tbl_reserves` (
  `ticket_id` int(255) NOT NULL,
  `passenger_id` int(11) NOT NULL,
  `route_id` int(11) NOT NULL,
  `bus_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_reserves`
--

INSERT INTO `tbl_reserves` (`ticket_id`, `passenger_id`, `route_id`, `bus_id`) VALUES
(1, 1, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_route`
--

CREATE TABLE `tbl_route` (
  `route_id` int(11) NOT NULL,
  `origin` varchar(255) NOT NULL,
  `destination` varchar(255) NOT NULL,
  `depart_date` date NOT NULL,
  `depart_time` time NOT NULL,
  `fare` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_route`
--

INSERT INTO `tbl_route` (`route_id`, `origin`, `destination`, `depart_date`, `depart_time`, `fare`) VALUES
(1, 'aparri', 'tuguegarao', '2024-04-10', '15:35:46', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_bus`
--
ALTER TABLE `tbl_bus`
  ADD PRIMARY KEY (`bus_id`);

--
-- Indexes for table `tbl_passenger`
--
ALTER TABLE `tbl_passenger`
  ADD PRIMARY KEY (`passenger_id`);

--
-- Indexes for table `tbl_reserves`
--
ALTER TABLE `tbl_reserves`
  ADD PRIMARY KEY (`ticket_id`),
  ADD UNIQUE KEY `passenger_id` (`passenger_id`,`route_id`,`bus_id`),
  ADD KEY `passenger_id_2` (`passenger_id`,`route_id`,`bus_id`),
  ADD KEY `route_id` (`route_id`),
  ADD KEY `bus_id` (`bus_id`);

--
-- Indexes for table `tbl_route`
--
ALTER TABLE `tbl_route`
  ADD PRIMARY KEY (`route_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbl_bus`
--
ALTER TABLE `tbl_bus`
  MODIFY `bus_id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tbl_passenger`
--
ALTER TABLE `tbl_passenger`
  MODIFY `passenger_id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tbl_reserves`
--
ALTER TABLE `tbl_reserves`
  MODIFY `ticket_id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tbl_route`
--
ALTER TABLE `tbl_route`
  MODIFY `route_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbl_reserves`
--
ALTER TABLE `tbl_reserves`
  ADD CONSTRAINT `tbl_reserves_ibfk_1` FOREIGN KEY (`passenger_id`) REFERENCES `tbl_passenger` (`passenger_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `tbl_reserves_ibfk_2` FOREIGN KEY (`route_id`) REFERENCES `tbl_route` (`route_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `tbl_reserves_ibfk_3` FOREIGN KEY (`bus_id`) REFERENCES `tbl_bus` (`bus_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
SET FOREIGN_KEY_CHECKS=1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
