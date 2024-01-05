-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 28, 2023 at 04:52 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.1.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `attendancesystem`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `id` int(11) NOT NULL,
  `userid` int(11) DEFAULT NULL,
  `eventid` int(11) DEFAULT NULL,
  `status` varchar(20) NOT NULL,
  `timestamp` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`id`, `userid`, `eventid`, `status`, `timestamp`) VALUES
(2, 11, 3, 'Absen', '2023-12-25 20:36:59'),
(4, 11, 3, 'Status', '2023-12-25 21:50:17'),
(5, 10, 4, 'Hadir', '2023-12-25 21:54:50'),
(6, 10, 5, 'Hadir', '2023-12-25 21:55:58'),
(7, 11, 5, 'Status', '2023-12-25 21:56:09'),
(8, 11, 6, 'Izin', '2023-12-25 21:56:57'),
(9, 11, 6, 'Izin', '2023-12-25 21:56:52');

-- --------------------------------------------------------

--
-- Table structure for table `events`
--

CREATE TABLE `events` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `iduser` int(11) NOT NULL,
  `date` date NOT NULL,
  `location` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `events`
--

INSERT INTO `events` (`id`, `name`, `iduser`, `date`, `location`) VALUES
(3, 'gugu', 7, '2023-12-19', 'jogja'),
(4, 'Ipa dahsyar', 7, '2024-01-04', 'USA'),
(5, 'Kelas MTK', 8, '2023-12-25', 'Jogja'),
(6, 'Kelas PPL', 8, '2023-12-25', 'Jogja');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `nama` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `role` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `nama`, `email`, `password`, `role`) VALUES
(6, 'bubu', 'bubu@gmail.com', '$2a$10$2xoLSRW30PMp9JVy1uoCSOS3LjlqTlnSpLZhJO9FmnzsFE7Nuerq6', '1'),
(7, 'gg', 'g@gmail.com', '$2a$10$h8X5FSbn/LbQVl6hjSAnnONn4zhWdYdG0c1Lp3vsVNxee5l47zG8.', '2'),
(8, 'Yayan', 'yayan@gmail.com', '$2a$10$tqmGSYkcEMjvkL5q0/PnkOFhztL2rrRDDnkJVdjsYDrvzcyg6V.je', '2'),
(10, 'zildan', 'zildan@gmail.com', '$2a$10$4ZnGaKB4V2bp6Ka4Z/MmKuy3b1z1yZOpX6wJaWjObEe89OlnID.7y', '3'),
(11, 'Lusiana', 'lus@gmail.com', '$2a$10$vNVR/c5LK6xMAbJ4xypM3uNZbk1Qqx1Yf9pZYKbi4Wpl1o9CqKrUe', '3');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendance`
--
ALTER TABLE `attendance`
  ADD PRIMARY KEY (`id`),
  ADD KEY `UserID` (`userid`),
  ADD KEY `EventID` (`eventid`);

--
-- Indexes for table `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendance`
--
ALTER TABLE `attendance`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `events`
--
ALTER TABLE `events`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attendance`
--
ALTER TABLE `attendance`
  ADD CONSTRAINT `attendance_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`),
  ADD CONSTRAINT `attendance_ibfk_2` FOREIGN KEY (`EventID`) REFERENCES `events` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
