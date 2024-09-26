using System.Collections.Generic;
using UnityEngine;

public class piceDataCtrl : MonoBehaviour
{
	private static piceDataCtrl instance_;

	private List<piceData> gs1_note_data_ = new List<piceData>
	{
		new piceData(0, 0, 0, 0, "/GS1/icon/", 18, 1, 0, 0, 0, 255),
		new piceData(1, 1, 1, 1, "/GS1/icon/", 23, 1, 1, 0, 0, 255),
		new piceData(2, 2, 2, 2, "/GS1/icon/", 19, 1, 2, 0, 0, 255),
		new piceData(3, 3, 3, 3, "/GS1/icon/", 20, 1, 3, 0, 0, 255),
		new piceData(4, 4, 4, 4, "/GS1/icon/", 21, 1, 4, 0, 0, 255),
		new piceData(5, 5, 5, 5, "/GS1/icon/", 22, 1, 5, 0, 0, 255),
		new piceData(6, 6, 6, 6, "/GS1/icon/", 16, 0, 6, 0, 0, 255),
		new piceData(7, 7, 7, 7, "/GS1/icon/", 14, 0, 7, 0, 0, 255),
		new piceData(8, 8, 8, 8, "/GS1/icon/", 15, 0, 8, 0, 0, 1),
		new piceData(9, 9, 9, 9, "/GS1/icon/", 17, 0, 9, 0, 0, 255),
		new piceData(0, 0, 0, 10, "/GS1/icon/", 18, 1, 10, 0, 0, 255),
		new piceData(1, 1, 1, 11, "/GS1/icon/", 23, 1, 11, 0, 0, 255),
		new piceData(1, 1, 1, 12, "/GS1/icon/", 23, 1, 12, 0, 0, 255),
		new piceData(1, 1, 1, 13, "/GS1/icon/", 23, 1, 13, 0, 0, 255),
		new piceData(10, 10, 10, 14, "/GS1/icon/", 30, 1, 14, 0, 0, 255),
		new piceData(11, 11, 11, 15, "/GS1/icon/", 10, 1, 15, 0, 0, 255),
		new piceData(12, 12, 12, 16, "/GS1/icon/", 11, 1, 16, 0, 0, 255),
		new piceData(12, 12, 12, 17, "/GS1/icon/", 11, 1, 17, 0, 0, 255),
		new piceData(12, 12, 12, 18, "/GS1/icon/", 11, 1, 18, 0, 0, 255),
		new piceData(13, 13, 13, 19, "/GS1/icon/", 13, 1, 19, 0, 0, 255),
		new piceData(14, 14, 14, 20, "/GS1/icon/", 12, 1, 20, 0, 0, 255),
		new piceData(14, 14, 14, 21, "/GS1/icon/", 12, 1, 21, 0, 0, 255),
		new piceData(15, 15, 15, 22, "/GS1/icon/", 29, 1, 22, 0, 0, 255),
		new piceData(16, 16, 16, 23, "/GS1/icon/", 6, 0, 23, 0, 1, 255),
		new piceData(17, 17, 17, 24, "/GS1/icon/", 16, 0, 24, 0, 0, 255),
		new piceData(18, 18, 18, 25, "/GS1/icon/", 7, 0, 25, 0, 0, 255),
		new piceData(19, 19, 19, 26, "/GS1/icon/", 3, 0, 26, 2, 0, 255),
		new piceData(20, 20, 20, 27, "/GS1/icon/", 26, 0, 27, 0, 0, 255),
		new piceData(21, 21, 21, 28, "/GS1/icon/", 14, 0, 28, 0, 0, 255),
		new piceData(21, 21, 21, 29, "/GS1/icon/", 14, 0, 29, 0, 0, 255),
		new piceData(22, 22, 22, 30, "/GS1/icon/", 24, 0, 30, 0, 0, 2),
		new piceData(22, 22, 22, 31, "/GS1/icon/", 8, 0, 31, 0, 0, 255),
		new piceData(23, 23, 23, 32, "/GS1/icon/", 2, 0, 32, 0, 0, 255),
		new piceData(24, 24, 24, 33, "/GS1/icon/", 17, 0, 33, 0, 0, 255),
		new piceData(25, 25, 25, 34, "/GS1/icon/", 28, 0, 34, 0, 0, 255),
		new piceData(25, 25, 25, 35, "/GS1/icon/", 13, 0, 35, 0, 0, 255),
		new piceData(26, 26, 26, 36, "/GS1/icon/", 27, 0, 36, 0, 0, 3),
		new piceData(27, 27, 27, 37, "/GS1/icon/", 9, 0, 37, 1, 0, 255),
		new piceData(28, 28, 28, 38, "/GS1/icon/", 17, 0, 38, 0, 0, 255),
		new piceData(17, 17, 17, 39, "/GS1/icon/", 16, 0, 39, 0, 0, 255),
		new piceData(29, 29, 29, 40, "/GS1/icon/", 7, 0, 40, 0, 0, 255),
		new piceData(30, 30, 30, 41, "/GS1/icon/", 1, 0, 41, 0, 0, 255),
		new piceData(31, 31, 31, 42, "/GS1/icon/", 43, 1, 42, 0, 0, 255),
		new piceData(32, 32, 32, 43, "/GS1/icon/", 48, 1, 43, 0, 0, 255),
		new piceData(33, 33, 33, 44, "/GS1/icon/", 47, 1, 44, 0, 0, 255),
		new piceData(34, 34, 34, 45, "/GS1/icon/", 45, 1, 45, 0, 0, 255),
		new piceData(35, 35, 35, 46, "/GS1/icon/", 46, 1, 46, 0, 0, 255),
		new piceData(36, 36, 36, 47, "/GS1/icon/", 44, 1, 47, 0, 0, 255),
		new piceData(37, 37, 37, 48, "/GS1/icon/", 42, 1, 48, 0, 0, 255),
		new piceData(38, 38, 38, 49, "/GS1/icon/", 17, 0, 49, 0, 0, 255),
		new piceData(39, 39, 39, 50, "/GS1/icon/", 16, 0, 50, 0, 0, 255),
		new piceData(40, 40, 40, 51, "/GS1/icon/", 37, 0, 51, 0, 0, 255),
		new piceData(41, 41, 41, 52, "/GS1/icon/", 64, 0, 52, 3, 0, 255),
		new piceData(41, 41, 41, 53, "/GS1/icon/", 64, 0, 53, 3, 0, 255),
		new piceData(27, 86, 86, 54, "/GS1/icon/", 9, 0, 54, 4, 0, 255),
		new piceData(42, 42, 42, 55, "/GS1/icon/", 25, 0, 55, 0, 0, 255),
		new piceData(42, 42, 42, 56, "/GS1/icon/", 25, 0, 56, 0, 0, 255),
		new piceData(43, 43, 43, 57, "/GS1/icon/", 33, 0, 57, 0, 0, 4),
		new piceData(44, 44, 44, 58, "/GS1/icon/", 38, 0, 58, 0, 0, 255),
		new piceData(45, 45, 45, 59, "/GS1/icon/", 7, 0, 59, 0, 0, 255),
		new piceData(46, 46, 46, 60, "/GS1/icon/", 34, 0, 60, 0, 0, 5),
		new piceData(47, 47, 47, 61, "/GS1/icon/", 39, 0, 61, 0, 0, 17),
		new piceData(48, 48, 48, 62, "/GS1/icon/", 31, 0, 62, 0, 0, 255),
		new piceData(49, 49, 49, 63, "/GS1/icon/", 32, 0, 63, 0, 0, 255),
		new piceData(50, 50, 50, 64, "/GS1/icon/", 40, 0, 64, 0, 0, 6),
		new piceData(51, 51, 51, 65, "/GS1/icon/", 35, 0, 65, 0, 0, 255),
		new piceData(52, 52, 52, 66, "/GS1/icon/", 36, 0, 66, 0, 0, 255),
		new piceData(53, 53, 53, 67, "/GS1/icon/", 33, 0, 67, 0, 0, 4),
		new piceData(54, 54, 54, 68, "/GS1/icon/", 66, 0, 68, 5, 0, 255),
		new piceData(55, 55, 55, 69, "/GS1/icon/", 41, 0, 69, 0, 0, 255),
		new piceData(56, 56, 56, 70, "/GS1/icon/", 0, 0, 70, 0, 0, 255),
		new piceData(10, 10, 10, 71, "/GS1/icon/", 30, 1, 71, 0, 0, 255),
		new piceData(57, 57, 57, 72, "/GS1/icon/", 58, 1, 72, 0, 0, 255),
		new piceData(57, 57, 57, 73, "/GS1/icon/", 58, 1, 73, 0, 0, 255),
		new piceData(2, 2, 2, 74, "/GS1/icon/", 19, 1, 74, 0, 0, 255),
		new piceData(58, 58, 58, 75, "/GS1/icon/", 61, 1, 75, 0, 0, 255),
		new piceData(59, 59, 59, 76, "/GS1/icon/", 28, 1, 76, 0, 0, 255),
		new piceData(60, 60, 60, 77, "/GS1/icon/", 59, 1, 77, 0, 0, 255),
		new piceData(61, 61, 61, 78, "/GS1/icon/", 60, 1, 78, 0, 0, 255),
		new piceData(62, 62, 62, 79, "/GS1/icon/", 62, 1, 79, 0, 0, 255),
		new piceData(63, 63, 63, 80, "/GS1/icon/", 62, 1, 80, 0, 0, 255),
		new piceData(64, 64, 64, 81, "/GS1/icon/", 49, 0, 81, 0, 0, 255),
		new piceData(65, 65, 65, 82, "/GS1/icon/", 16, 0, 82, 0, 0, 255),
		new piceData(66, 66, 66, 83, "/GS1/icon/", 16, 0, 83, 0, 0, 255),
		new piceData(66, 66, 66, 84, "/GS1/icon/", 16, 0, 84, 0, 0, 255),
		new piceData(67, 67, 67, 85, "/GS1/icon/", 1, 0, 85, 0, 0, 255),
		new piceData(68, 68, 68, 86, "/GS1/icon/", 67, 0, 86, 6, 0, 255),
		new piceData(68, 68, 68, 87, "/GS1/icon/", 68, 0, 87, 7, 0, 255),
		new piceData(69, 69, 69, 88, "/GS1/icon/", 9, 0, 88, 0, 0, 255),
		new piceData(70, 70, 70, 89, "/GS1/icon/", 28, 0, 89, 0, 0, 255),
		new piceData(71, 71, 71, 90, "/GS1/icon/", 4, 0, 90, 11, 0, 0),
		new piceData(72, 72, 72, 91, "/GS1/icon/", 17, 0, 91, 0, 0, 255),
		new piceData(27, 87, 87, 92, "/GS1/icon/", 9, 0, 92, 8, 0, 255),
		new piceData(73, 73, 73, 93, "/GS1/icon/", 5, 0, 93, 0, 0, 255),
		new piceData(73, 73, 73, 94, "/GS1/icon/", 5, 0, 94, 0, 0, 255),
		new piceData(74, 74, 74, 95, "/GS1/icon/", 50, 0, 95, 0, 0, 255),
		new piceData(75, 75, 75, 96, "/GS1/icon/", 17, 0, 96, 0, 0, 255),
		new piceData(76, 76, 76, 97, "/GS1/icon/", 51, 0, 97, 0, 0, 255),
		new piceData(77, 77, 77, 98, "/GS1/icon/", 53, 0, 98, 0, 0, 255),
		new piceData(78, 78, 78, 99, "/GS1/icon/", 56, 0, 99, 0, 0, 255),
		new piceData(79, 79, 79, 100, "/GS1/icon/", 52, 0, 100, 0, 0, 255),
		new piceData(80, 80, 80, 101, "/GS1/icon/", 57, 0, 101, 0, 0, 255),
		new piceData(81, 81, 81, 102, "/GS1/icon/", 63, 0, 102, 0, 0, 255),
		new piceData(82, 82, 82, 103, "/GS1/icon/", 16, 0, 103, 9, 0, 255),
		new piceData(83, 83, 83, 104, "/GS1/icon/", 69, 0, 104, 10, 0, 255),
		new piceData(84, 84, 84, 105, "/GS1/icon/", 7, 0, 105, 0, 0, 255),
		new piceData(85, 85, 85, 106, "/GS1/icon/", 55, 0, 106, 0, 0, 255),
		new piceData(77, 77, 77, 107, "/GS1/icon/", 54, 0, 107, 0, 0, 255),
		new piceData(114, 114, 114, 108, "/GS1/icon/", 131, 0, 108, 0, 116, 255),
		new piceData(115, 115, 115, 109, "/GS1/icon/", 132, 0, 109, 0, 117, 255),
		new piceData(116, 116, 116, 110, "/GS1/icon/", 133, 0, 110, 0, 120, 255),
		new piceData(117, 117, 117, 111, "/GS1/icon/", 134, 0, 111, 0, 119, 255),
		new piceData(118, 118, 118, 112, "/GS1/icon/", 135, 0, 112, 0, 121, 255),
		new piceData(119, 119, 119, 113, "/GS1/icon/", 136, 0, 113, 0, 192, 255),
		new piceData(120, 120, 120, 114, "/GS1/icon/", 137, 0, 114, 0, 123, 255),
		new piceData(121, 121, 121, 115, "/GS1/icon/", 138, 0, 115, 0, 15, 255),
		new piceData(134, 134, 134, 175, "/GS1/icon/", 119, 1, 116, 0, 0, 255),
		new piceData(135, 135, 135, 117, "/GS1/icon/", 120, 1, 117, 0, 0, 255),
		new piceData(135, 135, 135, 118, "/GS1/icon/", 120, 1, 118, 0, 0, 255),
		new piceData(136, 136, 136, 119, "/GS1/icon/", 121, 1, 119, 0, 0, 255),
		new piceData(137, 137, 137, 120, "/GS1/icon/", 122, 1, 120, 0, 0, 255),
		new piceData(139, 139, 139, 122, "/GS1/icon/", 125, 1, 121, 0, 0, 255),
		new piceData(139, 139, 139, 123, "/GS1/icon/", 125, 1, 122, 0, 0, 255),
		new piceData(140, 140, 140, 124, "/GS1/icon/", 124, 1, 123, 0, 0, 255),
		new piceData(141, 141, 141, 125, "/GS1/icon/", 123, 1, 124, 0, 0, 255),
		new piceData(88, 88, 88, 128, "/GS1/icon/", 70, 0, 125, 0, 2, 255),
		new piceData(89, 89, 89, 129, "/GS1/icon/", 72, 0, 126, 0, 4, 18),
		new piceData(90, 90, 90, 130, "/GS1/icon/", 73, 0, 127, 0, 5, 255),
		new piceData(91, 91, 91, 131, "/GS1/icon/", 74, 0, 128, 0, 6, 19),
		new piceData(92, 92, 92, 132, "/GS1/icon/", 75, 0, 129, 0, 7, 255),
		new piceData(92, 92, 92, 133, "/GS1/icon/", 75, 0, 130, 0, 7, 255),
		new piceData(93, 93, 93, 134, "/GS1/icon/", 76, 0, 131, 0, 8, 22),
		new piceData(94, 94, 94, 135, "/GS1/icon/", 16, 0, 132, 21, 0, 255),
		new piceData(95, 95, 95, 136, "/GS1/icon/", 77, 0, 133, 24, 0, 7),
		new piceData(96, 96, 96, 137, "/GS1/icon/", 78, 0, 134, 0, 9, 255),
		new piceData(96, 96, 96, 138, "/GS1/icon/", 78, 0, 135, 0, 9, 255),
		new piceData(97, 97, 97, 139, "/GS1/icon/", 78, 0, 136, 0, 9, 255),
		new piceData(98, 98, 98, 140, "/GS1/icon/", 9, 0, 137, 13, 0, 255),
		new piceData(99, 99, 99, 141, "/GS1/icon/", 146, 0, 138, 18, 0, 255),
		new piceData(99, 99, 99, 142, "/GS1/icon/", 146, 0, 139, 18, 0, 255),
		new piceData(100, 100, 100, 143, "/GS1/icon/", 80, 0, 140, 0, 11, 255),
		new piceData(101, 101, 101, 144, "/GS1/icon/", 113, 0, 141, 0, 14, 255),
		new piceData(101, 101, 101, 144, "/GS1/icon/", 113, 0, 142, 0, 14, 255),
		new piceData(101, 101, 101, 146, "/GS1/icon/", 113, 0, 143, 0, 14, 255),
		new piceData(102, 102, 102, 147, "/GS1/icon/", 84, 0, 144, 0, 15, 255),
		new piceData(103, 103, 103, 148, "/GS1/icon/", 85, 0, 145, 0, 16, 255),
		new piceData(104, 104, 104, 149, "/GS1/icon/", 87, 0, 146, 32, 0, 255),
		new piceData(104, 104, 104, 150, "/GS1/icon/", 87, 0, 147, 32, 0, 255),
		new piceData(105, 105, 105, 151, "/GS1/icon/", 118, 0, 148, 14, 0, 255),
		new piceData(105, 105, 105, 151, "/GS1/icon/", 118, 0, 149, 15, 0, 255),
		new piceData(105, 105, 105, 151, "/GS1/icon/", 118, 0, 150, 16, 0, 255),
		new piceData(106, 106, 106, 152, "/GS1/icon/", 111, 0, 151, 23, 0, 255),
		new piceData(107, 107, 107, 153, "/GS1/icon/", 9, 0, 152, 17, 0, 255),
		new piceData(108, 108, 108, 154, "/GS1/icon/", 88, 0, 153, 20, 0, 255),
		new piceData(109, 109, 109, 155, "/GS1/icon/", 92, 0, 154, 0, 20, 255),
		new piceData(110, 110, 110, 156, "/GS1/icon/", 89, 0, 155, 0, 19, 8),
		new piceData(110, 110, 110, 157, "/GS1/icon/", 90, 0, 156, 0, 18, 9),
		new piceData(111, 111, 111, 158, "/GS1/icon/", 86, 0, 157, 0, 17, 255),
		new piceData(112, 112, 112, 159, "/GS1/icon/", 93, 0, 158, 0, 21, 255),
		new piceData(113, 113, 113, 160, "/GS1/icon/", 110, 0, 159, 25, 0, 255),
		new piceData(113, 113, 113, 161, "/GS1/icon/", 110, 0, 160, 25, 0, 255),
		new piceData(123, 123, 123, 162, "/GS1/icon/", 16, 0, 161, 0, 0, 255),
		new piceData(124, 124, 124, 163, "/GS1/icon/", 95, 0, 162, 31, 0, 255),
		new piceData(125, 125, 125, 164, "/GS1/icon/", 112, 0, 163, 12, 0, 255),
		new piceData(126, 126, 126, 165, "/GS1/icon/", 116, 0, 164, 26, 0, 255),
		new piceData(127, 127, 127, 166, "/GS1/icon/", 16, 0, 165, 22, 0, 255),
		new piceData(128, 128, 128, 167, "/GS1/icon/", 96, 0, 166, 0, 23, 10),
		new piceData(129, 129, 129, 169, "/GS1/icon/", 72, 0, 167, 0, 24, 20),
		new piceData(130, 130, 130, 170, "/GS1/icon/", 140, 0, 168, 0, 27, 255),
		new piceData(130, 130, 130, 171, "/GS1/icon/", 115, 0, 169, 28, 0, 255),
		new piceData(131, 131, 131, 172, "/GS1/icon/", 98, 0, 170, 0, 25, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 79, 0, 171, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 81, 0, 172, 0, 12, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 83, 0, 173, 0, 0, 255),
		new piceData(110, 110, 110, 0, "/GS1/icon/", 90, 0, 174, 0, 0, 9),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 91, 0, 175, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 94, 0, 176, 0, 0, 255),
		new piceData(130, 130, 130, 0, "/GS1/icon/", 97, 0, 177, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 99, 0, 178, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 100, 0, 179, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 101, 0, 180, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 102, 0, 181, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 103, 0, 182, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 104, 0, 183, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 106, 0, 184, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 107, 0, 185, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 108, 0, 186, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 109, 0, 187, 0, 0, 21),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 83, 0, 188, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 114, 0, 189, 0, 0, 11),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 117, 0, 190, 0, 0, 255),
		new piceData(105, 105, 105, 0, "/GS1/icon/", 118, 0, 191, 0, 0, 255),
		new piceData(138, 138, 138, 121, "/GS1/icon/", 126, 1, 192, 0, 0, 255),
		new piceData(142, 142, 142, 126, "/GS1/icon/", 127, 1, 193, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 139, 0, 194, 0, 0, 255),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 128, 0, 195, 0, 0, 255),
		new piceData(101, 101, 101, 176, "/GS1/icon/", 82, 0, 196, 0, 13, 255),
		new piceData(133, 133, 133, 174, "/GS1/icon/", 145, 0, 197, 27, 0, 255),
		new piceData(132, 132, 132, 173, "/GS1/icon/", 130, 0, 198, 0, 28, 13),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 129, 0, 199, 29, 0, 12),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 142, 0, 200, 0, 42, 15),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 143, 0, 201, 0, 0, 255),
		new piceData(16, 16, 16, 127, "/GS1/icon/", 6, 0, 202, 0, 1, 255),
		new piceData(95, 95, 95, 136, "/GS1/icon/", 144, 0, 203, 33, 0, 16),
		new piceData(0, 0, 0, 0, "/GS1/icon/", 141, 0, 204, 0, 0, 14),
		new piceData(110, 110, 110, 177, "/GS1/icon/", 90, 0, 205, 0, 18, 9),
		new piceData(132, 132, 132, 173, "/GS1/icon/", 130, 0, 206, 29, 0, 13),
		new piceData(134, 134, 134, 116, "/GS1/icon/", 119, 1, 207, 0, 0, 255),
		new piceData(96, 143, 143, 176, "/GS1/icon/", 78, 0, 208, 0, 9, 255)
	};

	private List<piceData> gs2_note_data_ = new List<piceData>
	{
		new piceData(0, 0, 0, 0, "/GS2/icon/", 8, 1, 0, 0, 0, 255),
		new piceData(1, 1, 1, 1, "/GS2/icon/", 10, 1, 1, 0, 0, 255),
		new piceData(2, 2, 2, 2, "/GS2/icon/", 11, 1, 2, 0, 0, 255),
		new piceData(3, 3, 3, 3, "/GS2/icon/", 12, 1, 3, 0, 0, 255),
		new piceData(4, 4, 4, 4, "/GS2/icon/", 7, 1, 4, 0, 0, 255),
		new piceData(5, 5, 5, 5, "/GS2/icon/", 63, 0, 5, 0, 0, 255),
		new piceData(6, 6, 6, 6, "/GS2/icon/", 5, 0, 6, 0, 0, 255),
		new piceData(7, 7, 7, 7, "/GS2/icon/", 0, 0, 7, 0, 0, 255),
		new piceData(8, 8, 8, 8, "/GS2/icon/", 1, 0, 8, 1, 0, 255),
		new piceData(9, 9, 9, 9, "/GS2/icon/", 2, 0, 9, 0, 0, 255),
		new piceData(10, 10, 10, 10, "/GS2/icon/", 1, 0, 10, 2, 0, 255),
		new piceData(11, 11, 11, 11, "/GS2/icon/", 3, 0, 11, 0, 0, 255),
		new piceData(12, 12, 12, 12, "/GS2/icon/", 4, 0, 12, 0, 0, 255),
		new piceData(12, 12, 12, 13, "/GS2/icon/", 4, 0, 13, 0, 0, 255),
		new piceData(6, 6, 6, 14, "/GS2/icon/", 5, 0, 14, 0, 0, 255),
		new piceData(13, 13, 13, 15, "/GS2/icon/", 13, 0, 15, 0, 0, 0),
		new piceData(14, 14, 14, 16, "/GS2/icon/", 13, 0, 16, 0, 0, 0),
		new piceData(15, 15, 15, 17, "/GS2/icon/", 6, 0, 17, 0, 0, 255),
		new piceData(16, 16, 16, 18, "/GS2/icon/", 9, 1, 18, 0, 0, 255),
		new piceData(17, 17, 17, 19, "/GS2/icon/", 30, 1, 19, 0, 0, 255),
		new piceData(17, 17, 17, 20, "/GS2/icon/", 30, 1, 20, 0, 0, 255),
		new piceData(18, 18, 18, 21, "/GS2/icon/", 31, 1, 21, 0, 0, 255),
		new piceData(18, 18, 18, 22, "/GS2/icon/", 31, 1, 22, 0, 0, 255),
		new piceData(19, 19, 19, 23, "/GS2/icon/", 7, 1, 23, 0, 0, 255),
		new piceData(19, 19, 19, 24, "/GS2/icon/", 7, 1, 24, 0, 0, 255),
		new piceData(20, 20, 20, 25, "/GS2/icon/", 37, 1, 25, 0, 0, 255),
		new piceData(21, 21, 21, 26, "/GS2/icon/", 32, 1, 26, 0, 0, 255),
		new piceData(22, 22, 22, 27, "/GS2/icon/", 33, 1, 27, 0, 0, 255),
		new piceData(22, 22, 22, 28, "/GS2/icon/", 33, 1, 28, 0, 0, 255),
		new piceData(23, 23, 23, 29, "/GS2/icon/", 34, 1, 29, 0, 0, 255),
		new piceData(24, 24, 24, 30, "/GS2/icon/", 35, 1, 30, 0, 0, 255),
		new piceData(24, 24, 24, 31, "/GS2/icon/", 35, 1, 31, 0, 0, 255),
		new piceData(25, 25, 25, 32, "/GS2/icon/", 40, 1, 32, 0, 0, 255),
		new piceData(26, 26, 26, 33, "/GS2/icon/", 41, 1, 33, 0, 0, 255),
		new piceData(27, 27, 27, 34, "/GS2/icon/", 39, 1, 34, 0, 0, 255),
		new piceData(28, 28, 28, 35, "/GS2/icon/", 1, 0, 35, 3, 0, 255),
		new piceData(29, 29, 29, 36, "/GS2/icon/", 1, 0, 36, 4, 0, 255),
		new piceData(30, 30, 30, 37, "/GS2/icon/", 0, 0, 37, 0, 0, 255),
		new piceData(30, 30, 30, 38, "/GS2/icon/", 0, 0, 38, 0, 0, 255),
		new piceData(31, 31, 31, 39, "/GS2/icon/", 29, 0, 39, 0, 0, 4),
		new piceData(32, 32, 32, 40, "/GS2/icon/", 14, 0, 40, 0, 0, 1),
		new piceData(32, 32, 32, 41, "/GS2/icon/", 14, 0, 41, 8, 0, 1),
		new piceData(33, 33, 33, 42, "/GS2/icon/", 27, 0, 42, 0, 0, 255),
		new piceData(33, 33, 33, 43, "/GS2/icon/", 28, 0, 43, 0, 0, 255),
		new piceData(34, 34, 34, 44, "/GS2/icon/", 15, 0, 44, 0, 0, 255),
		new piceData(35, 35, 35, 45, "/GS2/icon/", 16, 0, 45, 0, 0, 255),
		new piceData(36, 36, 122, 46, "/GS2/icon/", 18, 0, 46, 0, 0, 255),
		new piceData(37, 37, 37, 47, "/GS2/icon/", 19, 0, 47, 0, 0, 255),
		new piceData(38, 38, 38, 48, "/GS2/icon/", 20, 0, 48, 0, 0, 255),
		new piceData(38, 38, 38, 49, "/GS2/icon/", 20, 0, 49, 5, 0, 255),
		new piceData(39, 39, 39, 50, "/GS2/icon/", 21, 0, 50, 6, 0, 255),
		new piceData(40, 40, 40, 51, "/GS2/icon/", 21, 0, 51, 6, 0, 255),
		new piceData(41, 41, 41, 52, "/GS2/icon/", 21, 0, 52, 7, 0, 255),
		new piceData(42, 42, 42, 53, "/GS2/icon/", 22, 0, 53, 0, 0, 2),
		new piceData(42, 42, 42, 54, "/GS2/icon/", 22, 0, 54, 0, 0, 2),
		new piceData(43, 43, 43, 55, "/GS2/icon/", 23, 0, 55, 0, 0, 255),
		new piceData(44, 44, 44, 56, "/GS2/icon/", 24, 0, 56, 0, 0, 3),
		new piceData(45, 45, 45, 57, "/GS2/icon/", 25, 0, 57, 0, 0, 255),
		new piceData(46, 46, 46, 58, "/GS2/icon/", 35, 0, 58, 0, 0, 255),
		new piceData(47, 47, 47, 59, "/GS2/icon/", 38, 0, 59, 0, 0, 255),
		new piceData(19, 19, 19, 60, "/GS2/icon/", 7, 1, 60, 0, 0, 255),
		new piceData(48, 48, 48, 61, "/GS2/icon/", 61, 1, 61, 0, 0, 255),
		new piceData(25, 25, 25, 62, "/GS2/icon/", 40, 1, 62, 0, 0, 255),
		new piceData(49, 49, 49, 63, "/GS2/icon/", 67, 1, 63, 0, 0, 255),
		new piceData(50, 50, 50, 64, "/GS2/icon/", 62, 1, 64, 0, 0, 255),
		new piceData(51, 51, 51, 65, "/GS2/icon/", 64, 1, 65, 0, 0, 255),
		new piceData(51, 51, 51, 66, "/GS2/icon/", 64, 1, 66, 0, 0, 255),
		new piceData(52, 52, 52, 67, "/GS2/icon/", 65, 1, 67, 0, 0, 255),
		new piceData(53, 53, 53, 68, "/GS2/icon/", 66, 1, 68, 0, 0, 255),
		new piceData(53, 53, 53, 69, "/GS2/icon/", 66, 1, 69, 0, 0, 255),
		new piceData(54, 54, 54, 70, "/GS2/icon/", 68, 1, 70, 0, 0, 255),
		new piceData(55, 55, 55, 71, "/GS2/icon/", 1, 0, 71, 9, 0, 255),
		new piceData(56, 56, 56, 72, "/GS2/icon/", 42, 0, 72, 0, 0, 255),
		new piceData(57, 57, 57, 73, "/GS2/icon/", 1, 0, 73, 10, 0, 255),
		new piceData(58, 58, 58, 74, "/GS2/icon/", 4, 0, 74, 0, 0, 255),
		new piceData(59, 59, 59, 75, "/GS2/icon/", 43, 0, 75, 11, 0, 255),
		new piceData(60, 60, 60, 76, "/GS2/icon/", 44, 0, 76, 0, 0, 255),
		new piceData(61, 61, 61, 77, "/GS2/icon/", 45, 0, 77, 0, 0, 255),
		new piceData(62, 62, 62, 78, "/GS2/icon/", 46, 0, 78, 0, 0, 255),
		new piceData(63, 63, 63, 79, "/GS2/icon/", 0, 0, 79, 0, 0, 255),
		new piceData(64, 64, 64, 80, "/GS2/icon/", 48, 0, 80, 0, 0, 255),
		new piceData(65, 65, 65, 81, "/GS2/icon/", 49, 0, 81, 0, 0, 255),
		new piceData(66, 66, 66, 82, "/GS2/icon/", 47, 0, 82, 0, 0, 255),
		new piceData(67, 67, 67, 83, "/GS2/icon/", 1, 0, 83, 12, 0, 255),
		new piceData(68, 68, 68, 84, "/GS2/icon/", 1, 0, 84, 13, 0, 255),
		new piceData(69, 69, 69, 85, "/GS2/icon/", 51, 0, 85, 0, 0, 255),
		new piceData(69, 69, 69, 86, "/GS2/icon/", 52, 0, 86, 14, 0, 255),
		new piceData(69, 69, 69, 87, "/GS2/icon/", 52, 0, 87, 15, 0, 255),
		new piceData(69, 69, 69, 88, "/GS2/icon/", 52, 0, 88, 16, 0, 255),
		new piceData(70, 70, 70, 89, "/GS2/icon/", 54, 0, 89, 0, 0, 255),
		new piceData(71, 71, 71, 90, "/GS2/icon/", 53, 0, 90, 0, 0, 255),
		new piceData(72, 72, 72, 91, "/GS2/icon/", 55, 0, 91, 0, 0, 255),
		new piceData(73, 73, 73, 92, "/GS2/icon/", 58, 0, 92, 0, 0, 255),
		new piceData(74, 74, 74, 93, "/GS2/icon/", 52, 0, 93, 0, 0, 255),
		new piceData(75, 75, 75, 94, "/GS2/icon/", 59, 0, 94, 0, 0, 5),
		new piceData(76, 76, 76, 95, "/GS2/icon/", 57, 0, 95, 0, 0, 255),
		new piceData(77, 77, 77, 96, "/GS2/icon/", 56, 0, 96, 0, 0, 255),
		new piceData(78, 78, 78, 97, "/GS2/icon/", 60, 0, 97, 0, 0, 255),
		new piceData(79, 79, 79, 98, "/GS2/icon/", 69, 1, 98, 0, 0, 255),
		new piceData(80, 80, 80, 99, "/GS2/icon/", 70, 1, 99, 0, 0, 255),
		new piceData(21, 21, 21, 100, "/GS2/icon/", 32, 1, 100, 0, 0, 255),
		new piceData(81, 81, 81, 101, "/GS2/icon/", 71, 1, 101, 0, 0, 255),
		new piceData(82, 82, 82, 102, "/GS2/icon/", 72, 1, 102, 0, 0, 255),
		new piceData(82, 82, 82, 103, "/GS2/icon/", 72, 1, 103, 0, 0, 255),
		new piceData(83, 83, 83, 104, "/GS2/icon/", 73, 1, 104, 0, 0, 255),
		new piceData(84, 84, 84, 105, "/GS2/icon/", 74, 1, 105, 0, 0, 255),
		new piceData(84, 84, 84, 106, "/GS2/icon/", 74, 1, 106, 0, 0, 255),
		new piceData(85, 85, 85, 107, "/GS2/icon/", 75, 1, 107, 0, 0, 255),
		new piceData(86, 86, 86, 108, "/GS2/icon/", 76, 1, 108, 0, 0, 255),
		new piceData(87, 87, 87, 109, "/GS2/icon/", 77, 1, 109, 0, 0, 255),
		new piceData(88, 88, 88, 110, "/GS2/icon/", 78, 0, 110, 0, 0, 6),
		new piceData(89, 89, 89, 111, "/GS2/icon/", 79, 0, 111, 0, 0, 255),
		new piceData(89, 89, 89, 112, "/GS2/icon/", 79, 0, 112, 0, 0, 255),
		new piceData(90, 90, 90, 113, "/GS2/icon/", 94, 0, 113, 0, 0, 255),
		new piceData(90, 90, 90, 114, "/GS2/icon/", 94, 0, 114, 0, 0, 255),
		new piceData(91, 91, 91, 115, "/GS2/icon/", 1, 0, 115, 17, 0, 255),
		new piceData(92, 92, 92, 116, "/GS2/icon/", 80, 0, 116, 0, 0, 255),
		new piceData(92, 92, 92, 117, "/GS2/icon/", 80, 0, 117, 0, 0, 255),
		new piceData(93, 93, 93, 118, "/GS2/icon/", 81, 0, 118, 0, 0, 255),
		new piceData(94, 94, 94, 119, "/GS2/icon/", 103, 0, 119, 0, 0, 255),
		new piceData(55, 55, 55, 120, "/GS2/icon/", 1, 0, 120, 18, 0, 255),
		new piceData(95, 95, 95, 121, "/GS2/icon/", 84, 0, 121, 0, 0, 255),
		new piceData(96, 96, 96, 122, "/GS2/icon/", 84, 0, 122, 0, 0, 255),
		new piceData(97, 97, 97, 123, "/GS2/icon/", 82, 0, 123, 0, 0, 7),
		new piceData(98, 98, 98, 124, "/GS2/icon/", 83, 0, 124, 19, 0, 8),
		new piceData(99, 99, 99, 125, "/GS2/icon/", 0, 0, 125, 0, 0, 255),
		new piceData(37, 37, 37, 126, "/GS2/icon/", 105, 0, 126, 0, 0, 255),
		new piceData(39, 39, 39, 127, "/GS2/icon/", 1, 0, 127, 20, 0, 255),
		new piceData(100, 100, 100, 128, "/GS2/icon/", 85, 0, 128, 0, 0, 9),
		new piceData(101, 101, 101, 129, "/GS2/icon/", 38, 0, 129, 0, 0, 255),
		new piceData(102, 102, 102, 130, "/GS2/icon/", 86, 0, 130, 0, 0, 255),
		new piceData(103, 103, 103, 131, "/GS2/icon/", 92, 0, 131, 0, 0, 255),
		new piceData(104, 104, 104, 132, "/GS2/icon/", 87, 0, 132, 0, 0, 255),
		new piceData(105, 105, 105, 133, "/GS2/icon/", 88, 0, 133, 0, 0, 255),
		new piceData(106, 106, 106, 134, "/GS2/icon/", 89, 0, 134, 0, 0, 255),
		new piceData(106, 106, 106, 135, "/GS2/icon/", 89, 0, 135, 0, 0, 255),
		new piceData(107, 107, 107, 136, "/GS2/icon/", 90, 0, 136, 0, 0, 255),
		new piceData(108, 108, 108, 137, "/GS2/icon/", 38, 0, 137, 0, 0, 255),
		new piceData(109, 109, 109, 138, "/GS2/icon/", 38, 0, 138, 0, 0, 255),
		new piceData(110, 110, 110, 139, "/GS2/icon/", 104, 0, 139, 0, 0, 255),
		new piceData(111, 111, 111, 140, "/GS2/icon/", 104, 0, 140, 0, 0, 255),
		new piceData(112, 112, 112, 141, "/GS2/icon/", 106, 0, 141, 0, 0, 255),
		new piceData(113, 113, 113, 142, "/GS2/icon/", 95, 0, 142, 0, 0, 255),
		new piceData(114, 114, 114, 143, "/GS2/icon/", 93, 0, 143, 0, 0, 255),
		new piceData(115, 115, 115, 144, "/GS2/icon/", 98, 0, 144, 0, 0, 255),
		new piceData(115, 115, 115, 145, "/GS2/icon/", 98, 0, 145, 0, 0, 255),
		new piceData(116, 116, 116, 146, "/GS2/icon/", 100, 0, 146, 0, 0, 255),
		new piceData(116, 116, 116, 147, "/GS2/icon/", 100, 0, 147, 0, 0, 255),
		new piceData(36, 36, 36, 148, "/GS2/icon/", 101, 0, 148, 0, 0, 255),
		new piceData(36, 36, 36, 149, "/GS2/icon/", 101, 0, 149, 0, 0, 255),
		new piceData(117, 117, 117, 150, "/GS2/icon/", 102, 0, 150, 0, 0, 255),
		new piceData(118, 118, 118, 151, "/GS2/icon/", 96, 0, 151, 0, 0, 255),
		new piceData(119, 119, 119, 152, "/GS2/icon/", 97, 0, 152, 0, 0, 255),
		new piceData(120, 120, 120, 153, "/GS2/icon/", 99, 0, 153, 0, 0, 10),
		new piceData(121, 121, 121, 154, "/GS2/icon/", 1, 0, 154, 21, 0, 255),
		new piceData(93, 93, 93, 155, "/GS2/icon/", 81, 0, 155, 0, 0, 255)
	};

	private List<piceData> gs3_note_data_ = new List<piceData>
	{
		new piceData(0, 0, 0, 0, "/GS3/icon/", 9, 1, 0, 0, 0, 255),
		new piceData(1, 1, 1, 1, "/GS3/icon/", 10, 1, 1, 0, 0, 255),
		new piceData(2, 2, 2, 2, "/GS3/icon/", 8, 1, 2, 0, 0, 255),
		new piceData(3, 3, 3, 3, "/GS3/icon/", 12, 1, 3, 0, 0, 255),
		new piceData(4, 4, 4, 4, "/GS3/icon/", 11, 1, 4, 0, 0, 255),
		new piceData(5, 5, 5, 5, "/GS3/icon/", 13, 0, 5, 0, 0, 255),
		new piceData(6, 6, 6, 6, "/GS3/icon/", 0, 0, 6, 0, 0, 255),
		new piceData(7, 7, 7, 7, "/GS3/icon/", 1, 0, 7, 1, 0, 255),
		new piceData(8, 8, 8, 8, "/GS3/icon/", 1, 0, 8, 2, 0, 255),
		new piceData(9, 9, 9, 9, "/GS3/icon/", 6, 0, 9, 0, 0, 2),
		new piceData(10, 10, 10, 10, "/GS3/icon/", 5, 0, 10, 0, 0, 255),
		new piceData(11, 11, 11, 11, "/GS3/icon/", 3, 0, 11, 0, 0, 1),
		new piceData(12, 12, 12, 12, "/GS3/icon/", 7, 0, 12, 0, 0, 255),
		new piceData(13, 13, 13, 13, "/GS3/icon/", 2, 0, 13, 3, 0, 0),
		new piceData(14, 14, 14, 14, "/GS3/icon/", 3, 0, 14, 0, 0, 1),
		new piceData(15, 15, 15, 15, "/GS3/icon/", 4, 0, 15, 4, 0, 255),
		new piceData(16, 16, 16, 16, "/GS3/icon/", 38, 1, 16, 0, 0, 255),
		new piceData(17, 17, 17, 17, "/GS3/icon/", 31, 1, 17, 0, 0, 255),
		new piceData(18, 18, 18, 18, "/GS3/icon/", 39, 1, 18, 0, 0, 255),
		new piceData(19, 19, 19, 19, "/GS3/icon/", 32, 1, 19, 0, 0, 255),
		new piceData(20, 20, 20, 20, "/GS3/icon/", 33, 1, 20, 0, 0, 255),
		new piceData(21, 21, 21, 21, "/GS3/icon/", 42, 1, 21, 0, 0, 255),
		new piceData(22, 22, 22, 22, "/GS3/icon/", 35, 1, 22, 0, 0, 255),
		new piceData(23, 23, 23, 23, "/GS3/icon/", 37, 1, 23, 0, 0, 255),
		new piceData(23, 23, 23, 24, "/GS3/icon/", 37, 1, 24, 0, 0, 255),
		new piceData(24, 24, 24, 25, "/GS3/icon/", 34, 1, 25, 0, 0, 255),
		new piceData(24, 24, 24, 26, "/GS3/icon/", 34, 1, 26, 0, 0, 255),
		new piceData(25, 25, 25, 27, "/GS3/icon/", 36, 1, 27, 0, 0, 255),
		new piceData(25, 25, 25, 28, "/GS3/icon/", 36, 1, 28, 0, 0, 255),
		new piceData(26, 26, 26, 29, "/GS3/icon/", 40, 1, 29, 0, 0, 255),
		new piceData(26, 26, 26, 30, "/GS3/icon/", 40, 1, 30, 0, 0, 255),
		new piceData(27, 27, 27, 31, "/GS3/icon/", 44, 1, 31, 0, 0, 255),
		new piceData(5, 5, 5, 32, "/GS3/icon/", 13, 0, 32, 0, 0, 255),
		new piceData(28, 28, 28, 33, "/GS3/icon/", 27, 0, 33, 0, 0, 255),
		new piceData(29, 29, 29, 34, "/GS3/icon/", 1, 0, 34, 5, 0, 255),
		new piceData(30, 30, 30, 35, "/GS3/icon/", 14, 0, 35, 0, 0, 255),
		new piceData(30, 30, 30, 36, "/GS3/icon/", 14, 0, 36, 0, 0, 255),
		new piceData(30, 30, 30, 37, "/GS3/icon/", 14, 0, 37, 0, 0, 255),
		new piceData(31, 31, 31, 38, "/GS3/icon/", 15, 0, 38, 0, 0, 255),
		new piceData(31, 31, 31, 39, "/GS3/icon/", 15, 0, 39, 0, 0, 255),
		new piceData(31, 31, 31, 40, "/GS3/icon/", 16, 0, 40, 0, 0, 3),
		new piceData(31, 31, 31, 41, "/GS3/icon/", 16, 0, 41, 0, 0, 3),
		new piceData(31, 31, 31, 42, "/GS3/icon/", 16, 0, 42, 0, 0, 3),
		new piceData(32, 32, 32, 43, "/GS3/icon/", 17, 0, 43, 6, 0, 255),
		new piceData(33, 33, 33, 44, "/GS3/icon/", 19, 0, 44, 0, 0, 255),
		new piceData(33, 33, 33, 45, "/GS3/icon/", 19, 0, 45, 0, 0, 255),
		new piceData(34, 34, 34, 46, "/GS3/icon/", 20, 0, 46, 0, 0, 255),
		new piceData(35, 35, 35, 47, "/GS3/icon/", 28, 0, 47, 0, 0, 255),
		new piceData(36, 36, 36, 48, "/GS3/icon/", 1, 0, 48, 7, 0, 255),
		new piceData(37, 37, 37, 49, "/GS3/icon/", 18, 0, 49, 8, 0, 255),
		new piceData(37, 37, 37, 50, "/GS3/icon/", 18, 0, 50, 8, 0, 255),
		new piceData(38, 38, 38, 51, "/GS3/icon/", 21, 0, 51, 0, 0, 255),
		new piceData(38, 38, 38, 52, "/GS3/icon/", 21, 0, 52, 0, 0, 255),
		new piceData(39, 39, 39, 53, "/GS3/icon/", 22, 0, 53, 0, 0, 255),
		new piceData(39, 39, 39, 54, "/GS3/icon/", 22, 0, 54, 0, 0, 255),
		new piceData(40, 40, 40, 55, "/GS3/icon/", 22, 0, 55, 0, 0, 255),
		new piceData(41, 41, 41, 56, "/GS3/icon/", 1, 0, 56, 9, 0, 255),
		new piceData(42, 42, 42, 57, "/GS3/icon/", 23, 0, 57, 0, 0, 255),
		new piceData(43, 43, 43, 58, "/GS3/icon/", 24, 0, 58, 0, 0, 255),
		new piceData(44, 44, 44, 59, "/GS3/icon/", 43, 0, 59, 10, 0, 255),
		new piceData(45, 45, 45, 60, "/GS3/icon/", 0, 0, 60, 0, 0, 255),
		new piceData(13, 13, 13, 61, "/GS3/icon/", 2, 0, 61, 11, 0, 0),
		new piceData(13, 13, 13, 62, "/GS3/icon/", 2, 0, 62, 11, 0, 0),
		new piceData(46, 46, 46, 63, "/GS3/icon/", 20, 0, 63, 0, 0, 255),
		new piceData(47, 47, 47, 64, "/GS3/icon/", 3, 0, 64, 0, 0, 1),
		new piceData(48, 48, 48, 65, "/GS3/icon/", 25, 0, 65, 0, 0, 255),
		new piceData(48, 48, 48, 66, "/GS3/icon/", 25, 0, 66, 0, 0, 255),
		new piceData(49, 49, 49, 67, "/GS3/icon/", 26, 0, 67, 0, 0, 4),
		new piceData(50, 50, 50, 68, "/GS3/icon/", 41, 1, 68, 0, 0, 255),
		new piceData(51, 51, 51, 69, "/GS3/icon/", 45, 1, 69, 0, 0, 255),
		new piceData(52, 52, 52, 70, "/GS3/icon/", 46, 1, 70, 0, 0, 255),
		new piceData(53, 53, 53, 71, "/GS3/icon/", 29, 1, 71, 0, 0, 255),
		new piceData(54, 54, 54, 72, "/GS3/icon/", 30, 1, 72, 0, 0, 255),
		new piceData(55, 55, 55, 73, "/GS3/icon/", 29, 1, 73, 0, 0, 255),
		new piceData(56, 56, 56, 74, "/GS3/icon/", 69, 1, 74, 0, 0, 255),
		new piceData(57, 57, 57, 75, "/GS3/icon/", 77, 1, 75, 0, 0, 255),
		new piceData(57, 57, 57, 76, "/GS3/icon/", 77, 1, 76, 0, 0, 255),
		new piceData(57, 57, 57, 77, "/GS3/icon/", 77, 1, 77, 0, 0, 255),
		new piceData(58, 58, 58, 78, "/GS3/icon/", 75, 1, 78, 0, 0, 255),
		new piceData(58, 58, 58, 79, "/GS3/icon/", 75, 1, 79, 0, 0, 255),
		new piceData(59, 59, 59, 80, "/GS3/icon/", 70, 1, 80, 0, 0, 255),
		new piceData(59, 59, 59, 81, "/GS3/icon/", 70, 1, 81, 0, 0, 255),
		new piceData(60, 60, 60, 82, "/GS3/icon/", 76, 1, 82, 0, 0, 255),
		new piceData(61, 61, 61, 83, "/GS3/icon/", 72, 1, 83, 0, 0, 255),
		new piceData(62, 62, 62, 84, "/GS3/icon/", 72, 1, 84, 0, 0, 255),
		new piceData(63, 63, 63, 85, "/GS3/icon/", 81, 1, 85, 0, 0, 255),
		new piceData(64, 64, 64, 86, "/GS3/icon/", 71, 1, 86, 0, 0, 255),
		new piceData(28, 28, 28, 87, "/GS3/icon/", 27, 0, 87, 0, 0, 255),
		new piceData(65, 65, 65, 88, "/GS3/icon/", 54, 0, 88, 0, 0, 7),
		new piceData(66, 66, 66, 89, "/GS3/icon/", 50, 0, 89, 13, 0, 5),
		new piceData(66, 66, 66, 90, "/GS3/icon/", 50, 0, 90, 13, 0, 5),
		new piceData(66, 66, 66, 91, "/GS3/icon/", 50, 0, 91, 13, 0, 5),
		new piceData(67, 67, 67, 92, "/GS3/icon/", 51, 0, 92, 0, 0, 6),
		new piceData(68, 68, 68, 93, "/GS3/icon/", 52, 0, 93, 0, 0, 255),
		new piceData(69, 69, 69, 94, "/GS3/icon/", 68, 0, 94, 0, 0, 255),
		new piceData(69, 69, 69, 95, "/GS3/icon/", 68, 0, 95, 0, 0, 255),
		new piceData(69, 69, 69, 96, "/GS3/icon/", 68, 0, 96, 0, 0, 255),
		new piceData(70, 70, 70, 97, "/GS3/icon/", 55, 0, 97, 0, 0, 8),
		new piceData(71, 71, 71, 98, "/GS3/icon/", 78, 0, 98, 0, 0, 255),
		new piceData(72, 72, 72, 99, "/GS3/icon/", 56, 0, 99, 0, 0, 9),
		new piceData(73, 73, 73, 100, "/GS3/icon/", 0, 0, 100, 0, 0, 255),
		new piceData(74, 74, 74, 101, "/GS3/icon/", 1, 0, 101, 14, 0, 255),
		new piceData(75, 75, 75, 102, "/GS3/icon/", 57, 0, 102, 0, 0, 255),
		new piceData(76, 76, 76, 103, "/GS3/icon/", 60, 0, 103, 0, 0, 10),
		new piceData(77, 77, 77, 104, "/GS3/icon/", 1, 0, 104, 12, 0, 255),
		new piceData(78, 78, 78, 105, "/GS3/icon/", 79, 0, 105, 0, 0, 14),
		new piceData(79, 79, 79, 106, "/GS3/icon/", 59, 0, 106, 0, 0, 255),
		new piceData(80, 80, 80, 107, "/GS3/icon/", 58, 0, 107, 0, 0, 255),
		new piceData(81, 81, 81, 108, "/GS3/icon/", 56, 0, 108, 0, 0, 9),
		new piceData(82, 82, 82, 109, "/GS3/icon/", 61, 0, 109, 0, 0, 11),
		new piceData(83, 83, 83, 110, "/GS3/icon/", 80, 0, 110, 0, 0, 255),
		new piceData(83, 83, 83, 111, "/GS3/icon/", 80, 0, 111, 0, 0, 255),
		new piceData(84, 84, 84, 112, "/GS3/icon/", 63, 0, 112, 0, 0, 255),
		new piceData(85, 85, 85, 113, "/GS3/icon/", 64, 0, 113, 0, 0, 255),
		new piceData(86, 86, 86, 114, "/GS3/icon/", 65, 0, 114, 0, 0, 19),
		new piceData(86, 86, 86, 115, "/GS3/icon/", 65, 0, 115, 0, 0, 19),
		new piceData(86, 86, 86, 116, "/GS3/icon/", 65, 0, 116, 0, 0, 19),
		new piceData(87, 87, 87, 117, "/GS3/icon/", 3, 0, 117, 0, 0, 1),
		new piceData(88, 88, 88, 118, "/GS3/icon/", 74, 0, 118, 0, 0, 255),
		new piceData(89, 89, 89, 119, "/GS3/icon/", 66, 0, 119, 0, 0, 13),
		new piceData(90, 90, 90, 120, "/GS3/icon/", 62, 0, 120, 0, 0, 12),
		new piceData(91, 91, 91, 121, "/GS3/icon/", 82, 0, 121, 0, 0, 255),
		new piceData(92, 92, 92, 122, "/GS3/icon/", 73, 0, 122, 0, 0, 255),
		new piceData(93, 93, 93, 123, "/GS3/icon/", 53, 0, 123, 0, 0, 255),
		new piceData(94, 94, 94, 124, "/GS3/icon/", 48, 0, 124, 0, 0, 255),
		new piceData(95, 95, 95, 125, "/GS3/icon/", 49, 0, 125, 0, 0, 255),
		new piceData(96, 96, 96, 126, "/GS3/icon/", 88, 1, 126, 0, 0, 255),
		new piceData(97, 97, 97, 127, "/GS3/icon/", 91, 1, 127, 0, 0, 255),
		new piceData(98, 98, 98, 128, "/GS3/icon/", 86, 1, 128, 0, 0, 255),
		new piceData(99, 99, 99, 129, "/GS3/icon/", 11, 1, 129, 0, 0, 255),
		new piceData(100, 100, 100, 130, "/GS3/icon/", 87, 1, 130, 0, 0, 255),
		new piceData(101, 101, 101, 131, "/GS3/icon/", 103, 1, 131, 0, 0, 255),
		new piceData(102, 102, 102, 132, "/GS3/icon/", 89, 1, 132, 0, 0, 255),
		new piceData(102, 102, 102, 133, "/GS3/icon/", 89, 1, 133, 0, 0, 255),
		new piceData(5, 5, 5, 134, "/GS3/icon/", 13, 0, 134, 0, 0, 255),
		new piceData(103, 103, 103, 135, "/GS3/icon/", 0, 0, 135, 0, 0, 255),
		new piceData(104, 104, 104, 136, "/GS3/icon/", 1, 0, 136, 16, 0, 255),
		new piceData(105, 105, 105, 137, "/GS3/icon/", 3, 0, 137, 15, 0, 1),
		new piceData(74, 74, 144, 138, "/GS3/icon/", 1, 0, 138, 17, 0, 255),
		new piceData(106, 106, 106, 139, "/GS3/icon/", 83, 0, 139, 0, 0, 255),
		new piceData(107, 107, 107, 140, "/GS3/icon/", 1, 0, 140, 18, 0, 255),
		new piceData(107, 107, 107, 141, "/GS3/icon/", 1, 0, 141, 18, 0, 255),
		new piceData(108, 108, 108, 142, "/GS3/icon/", 84, 0, 142, 0, 0, 255),
		new piceData(108, 108, 108, 143, "/GS3/icon/", 84, 0, 143, 0, 0, 255),
		new piceData(109, 109, 109, 144, "/GS3/icon/", 85, 0, 144, 0, 0, 255),
		new piceData(16, 16, 16, 145, "/GS3/icon/", 38, 1, 145, 0, 0, 255),
		new piceData(19, 19, 19, 146, "/GS3/icon/", 32, 1, 146, 0, 0, 255),
		new piceData(19, 19, 19, 147, "/GS3/icon/", 32, 1, 147, 0, 0, 255),
		new piceData(20, 20, 20, 148, "/GS3/icon/", 33, 1, 148, 0, 0, 255),
		new piceData(20, 20, 20, 149, "/GS3/icon/", 33, 1, 149, 0, 0, 255),
		new piceData(110, 110, 110, 150, "/GS3/icon/", 92, 1, 150, 0, 0, 255),
		new piceData(110, 110, 110, 151, "/GS3/icon/", 92, 1, 151, 0, 0, 255),
		new piceData(110, 110, 110, 152, "/GS3/icon/", 92, 1, 152, 0, 0, 255),
		new piceData(111, 111, 111, 153, "/GS3/icon/", 93, 1, 153, 0, 0, 255),
		new piceData(111, 111, 111, 154, "/GS3/icon/", 93, 1, 154, 0, 0, 255),
		new piceData(112, 112, 112, 155, "/GS3/icon/", 93, 1, 155, 0, 0, 255),
		new piceData(113, 113, 113, 156, "/GS3/icon/", 94, 1, 156, 0, 0, 255),
		new piceData(113, 113, 113, 157, "/GS3/icon/", 94, 1, 157, 0, 0, 255),
		new piceData(114, 114, 114, 158, "/GS3/icon/", 95, 1, 158, 0, 0, 255),
		new piceData(26, 26, 26, 159, "/GS3/icon/", 95, 1, 159, 0, 0, 255),
		new piceData(114, 114, 114, 160, "/GS3/icon/", 95, 1, 160, 0, 0, 255),
		new piceData(17, 17, 17, 161, "/GS3/icon/", 31, 1, 161, 0, 0, 255),
		new piceData(115, 115, 115, 162, "/GS3/icon/", 96, 1, 162, 0, 0, 255),
		new piceData(115, 115, 115, 163, "/GS3/icon/", 96, 1, 163, 0, 0, 255),
		new piceData(116, 116, 116, 164, "/GS3/icon/", 105, 1, 164, 0, 0, 255),
		new piceData(117, 117, 117, 165, "/GS3/icon/", 104, 1, 165, 0, 0, 255),
		new piceData(118, 118, 118, 166, "/GS3/icon/", 106, 1, 166, 0, 0, 255),
		new piceData(102, 102, 102, 167, "/GS3/icon/", 11, 1, 167, 0, 0, 255),
		new piceData(5, 5, 5, 168, "/GS3/icon/", 13, 0, 168, 0, 0, 255),
		new piceData(28, 28, 28, 169, "/GS3/icon/", 27, 0, 169, 0, 0, 255),
		new piceData(119, 119, 119, 170, "/GS3/icon/", 97, 0, 170, 19, 0, 15),
		new piceData(120, 120, 120, 171, "/GS3/icon/", 1, 0, 171, 20, 0, 255),
		new piceData(121, 121, 121, 172, "/GS3/icon/", 98, 0, 172, 21, 0, 255),
		new piceData(121, 121, 121, 173, "/GS3/icon/", 107, 0, 173, 28, 0, 255),
		new piceData(122, 122, 122, 174, "/GS3/icon/", 108, 0, 174, 22, 0, 255),
		new piceData(123, 123, 123, 175, "/GS3/icon/", 99, 0, 175, 0, 0, 255),
		new piceData(124, 124, 124, 176, "/GS3/icon/", 100, 0, 176, 23, 0, 255),
		new piceData(125, 125, 125, 177, "/GS3/icon/", 3, 0, 177, 24, 0, 1),
		new piceData(33, 33, 33, 178, "/GS3/icon/", 101, 0, 178, 0, 0, 255),
		new piceData(33, 33, 33, 179, "/GS3/icon/", 101, 0, 179, 0, 0, 255),
		new piceData(126, 126, 126, 180, "/GS3/icon/", 102, 0, 180, 0, 0, 255),
		new piceData(126, 126, 126, 181, "/GS3/icon/", 109, 0, 181, 0, 0, 255),
		new piceData(126, 126, 126, 182, "/GS3/icon/", 109, 0, 182, 0, 0, 255),
		new piceData(127, 127, 127, 183, "/GS3/icon/", 0, 0, 183, 0, 0, 255),
		new piceData(128, 128, 128, 184, "/GS3/icon/", 3, 0, 184, 0, 0, 1),
		new piceData(129, 129, 129, 185, "/GS3/icon/", 1, 0, 185, 25, 0, 255),
		new piceData(74, 74, 74, 186, "/GS3/icon/", 1, 0, 186, 26, 0, 255),
		new piceData(130, 130, 130, 187, "/GS3/icon/", 113, 0, 187, 27, 0, 255),
		new piceData(131, 131, 131, 188, "/GS3/icon/", 110, 0, 188, 0, 0, 255),
		new piceData(131, 131, 131, 189, "/GS3/icon/", 110, 0, 189, 0, 0, 255),
		new piceData(132, 132, 132, 190, "/GS3/icon/", 111, 0, 190, 0, 0, 255),
		new piceData(133, 133, 133, 191, "/GS3/icon/", 112, 0, 191, 29, 0, 16),
		new piceData(134, 134, 134, 192, "/GS3/icon/", 117, 1, 192, 0, 0, 255),
		new piceData(135, 135, 135, 193, "/GS3/icon/", 114, 0, 193, 0, 0, 17),
		new piceData(18, 18, 18, 194, "/GS3/icon/", 39, 1, 194, 0, 0, 255),
		new piceData(136, 136, 136, 195, "/GS3/icon/", 116, 0, 195, 0, 0, 255),
		new piceData(137, 137, 137, 196, "/GS3/icon/", 115, 0, 196, 0, 0, 255),
		new piceData(138, 138, 138, 197, "/GS3/icon/", 119, 0, 197, 0, 0, 255),
		new piceData(139, 139, 139, 198, "/GS3/icon/", 118, 0, 198, 0, 0, 18),
		new piceData(140, 140, 140, 199, "/GS3/icon/", 1, 0, 199, 30, 0, 255),
		new piceData(120, 120, 120, 171, "/GS3/icon/", 1, 0, 200, 31, 0, 255),
		new piceData(120, 120, 120, 171, "/GS3/icon/", 1, 0, 201, 32, 0, 255),
		new piceData(120, 120, 120, 171, "/GS3/icon/", 1, 0, 202, 33, 0, 255),
		new piceData(120, 120, 120, 171, "/GS3/icon/", 1, 0, 203, 34, 0, 255),
		new piceData(141, 141, 141, 204, "/GS3/icon/", 120, 0, 204, 0, 0, 255),
		new piceData(142, 142, 142, 205, "/GS3/icon/", 121, 0, 205, 0, 0, 255),
		new piceData(123, 123, 123, 206, "/GS3/icon/", 99, 0, 206, 0, 0, 255),
		new piceData(113, 113, 113, 156, "/GS3/icon/", 122, 1, 207, 0, 0, 255),
		new piceData(133, 133, 133, 208, "/GS3/icon/", 112, 0, 208, 29, 0, 16),
		new piceData(113, 113, 113, 209, "/GS3/icon/", 94, 1, 209, 0, 0, 255),
		new piceData(138, 138, 138, 210, "/GS3/icon/", 119, 0, 210, 0, 0, 255)
	};

	private readonly List<string> gs1_icon_file_ = new List<string>
	{
		"000", "001", "002", "003", "004", "005", "006", "007", "008", "009",
		"00a", "00b", "00c", "00d", "00e", "00f", "010", "011", "012", "013",
		"014", "015", "016", "017", "018", "019", "01a", "01b", "01c", "01d",
		"01e", "01f", "020", "021", "022", "023", "024", "025", "026", "027",
		"028", "029", "02a", "02b", "02c", "02d", "02e", "02f", "030", "031",
		"032", "033", "034", "035", "036", "037", "038", "039", "03a", "03b",
		"03c", "03d", "03e", "03f", "040", "041", "042", "043", "044", "045",
		"046", "047", "048", "049", "04a", "04b", "04c", "04d", "04e", "04f",
		"050", "051", "052", "053", "054", "055", "056", "057", "058", "059",
		"05a", "05b", "05c", "05d", "05e", "05f", "060", "061", "062", "063",
		"064", "065", "066", "067", "068", "069", "06a", "06b", "06c", "06d",
		"06e", "06f", "070", "071", "072", "073", "074", "075", "076", "077",
		"078", "079", "07a", "07b", "07c", "07d", "07e", "07f", "080", "081",
		"082", "083", "084", "085", "086", "087", "088", "089", "08a", "08b",
		"08c", "08d", "08e", "08f", "090", "091", "092"
	};

	private readonly List<string> gs2_icon_file_ = new List<string>
	{
		"000", "001", "002", "003", "004", "005", "006", "007", "008", "009",
		"00a", "00b", "00c", "00d", "00e", "00f", "010", "011", "012", "013",
		"014", "015", "016", "017", "018", "019", "01a", "01b", "01c", "01d",
		"01e", "01f", "020", "021", "022", "023", "024", "025", "026", "027",
		"028", "029", "02a", "02b", "02c", "02d", "02e", "02f", "030", "031",
		"032", "033", "034", "035", "036", "037", "038", "039", "03a", "03b",
		"03c", "03d", "03e", "03f", "040", "041", "042", "043", "044", "045",
		"046", "047", "048", "049", "04a", "04b", "04c", "04d", "04e", "04f",
		"050", "051", "052", "053", "054", "055", "056", "057", "058", "059",
		"05a", "05b", "05c", "05d", "05e", "05f", "060", "061", "062", "063",
		"064", "065", "066", "067", "068", "069", "06a"
	};

	private readonly List<string> gs3_icon_file_ = new List<string>
	{
		"000", "001", "002", "003", "004", "005", "006", "007", "008", "009",
		"00a", "00b", "00c", "00d", "010", "011", "012", "013", "014", "015",
		"016", "017", "018", "019", "01a", "01b", "01c", "01d", "01e", "01f",
		"020", "021", "022", "023", "024", "025", "026", "027", "028", "029",
		"02a", "02b", "02c", "02d", "02e", "02f", "030", "031", "032", "033",
		"034", "035", "036", "037", "038", "039", "03a", "03b", "03c", "03d",
		"03e", "03f", "040", "041", "042", "043", "044", "045", "046", "047",
		"048", "049", "04a", "04b", "04c", "04d", "04e", "04f", "050", "051",
		"052", "053", "054", "055", "056", "057", "058", "059", "05a", "05b",
		"05c", "05d", "05e", "05f", "060", "061", "062", "063", "064", "065",
		"066", "067", "068", "069", "06a", "06b", "06c", "06d", "001", "06f",
		"070", "071", "072", "073", "074", "075", "076", "077", "078", "079",
		"07a", "07b", "07c", "07d"
	};

	private readonly List<string> gs1_icon_file_language_ = new List<string>
	{
		"004", "00f", "018", "01b", "021", "022", "028", "04d", "059", "05a",
		"060", "072", "081", "082", "08d", "08e", "090", "027", "048", "04a",
		"061", "06d", "04c"
	};

	private readonly List<string> gs2_icon_file_language_ = new List<string>
	{
		"00d", "00e", "016", "018", "01d", "03b", "04e", "052", "053", "055",
		"063"
	};

	private readonly List<string> gs3_icon_file_language_ = new List<string>
	{
		"002", "003", "006", "012", "01c", "034", "035", "038", "039", "03a",
		"03e", "03f", "040", "044", "051", "063", "072", "074", "078", "043"
	};

	private List<piceDetailData> gs1_status_ext_bg_tbl_ = new List<piceDetailData>
	{
		new piceDetailData(29u, 0u, 0u, string.Empty),
		new piceDetailData(29u, 0u, 0u, string.Empty),
		new piceDetailData(45u, 3u, 0u, string.Empty),
		new piceDetailData(49u, 0u, 0u, string.Empty),
		new piceDetailData(61u, 0u, 0u, string.Empty),
		new piceDetailData(57u, 0u, 0u, string.Empty),
		new piceDetailData(94u, 0u, 0u, string.Empty),
		new piceDetailData(87u, 0u, 0u, string.Empty),
		new piceDetailData(86u, 0u, 0u, string.Empty),
		new piceDetailData(82u, 3u, 0u, string.Empty),
		new piceDetailData(88u, 0u, 0u, string.Empty),
		new piceDetailData(89u, 0u, 0u, string.Empty),
		new piceDetailData(163u, 4u, 0u, string.Empty),
		new piceDetailData(119u, 0u, 2u, string.Empty),
		new piceDetailData(141u, 0u, 0u, string.Empty),
		new piceDetailData(161u, 0u, 0u, string.Empty),
		new piceDetailData(162u, 0u, 0u, string.Empty),
		new piceDetailData(159u, 0u, 0u, string.Empty),
		new piceDetailData(121u, 0u, 0u, string.Empty),
		new piceDetailData(123u, 0u, 0u, string.Empty),
		new piceDetailData(213u, 0u, 0u, string.Empty),
		new piceDetailData(214u, 0u, 0u, string.Empty),
		new piceDetailData(215u, 0u, 0u, string.Empty),
		new piceDetailData(216u, 0u, 0u, string.Empty),
		new piceDetailData(124u, 0u, 0u, string.Empty),
		new piceDetailData(129u, 0u, 0u, string.Empty),
		new piceDetailData(185u, 0u, 0u, string.Empty),
		new piceDetailData(199u, 0u, 0u, string.Empty),
		new piceDetailData(189u, 0u, 0u, string.Empty),
		new piceDetailData(217u, 0u, 0u, string.Empty),
		new piceDetailData(0u, 0u, 0u, string.Empty),
		new piceDetailData(0u, 255u, 1u, string.Empty),
		new piceDetailData(231u, 0u, 0u, string.Empty),
		new piceDetailData(242u, 0u, 0u, string.Empty),
		new piceDetailData(45u, 0u, 0u, string.Empty),
		new piceDetailData(46u, 0u, 0u, string.Empty),
		new piceDetailData(48u, 0u, 0u, string.Empty),
		new piceDetailData(82u, 0u, 0u, string.Empty),
		new piceDetailData(83u, 0u, 0u, string.Empty),
		new piceDetailData(84u, 0u, 0u, string.Empty),
		new piceDetailData(163u, 0u, 0u, string.Empty),
		new piceDetailData(164u, 0u, 0u, string.Empty),
		new piceDetailData(165u, 0u, 0u, string.Empty),
		new piceDetailData(166u, 0u, 0u, string.Empty)
	};

	private List<piceDetailData> gs2_status_ext_bg_tbl_ = new List<piceDetailData>
	{
		new piceDetailData(21u, 0u, 0u, string.Empty),
		new piceDetailData(21u, 0u, 0u, string.Empty),
		new piceDetailData(22u, 0u, 0u, string.Empty),
		new piceDetailData(43u, 0u, 0u, string.Empty),
		new piceDetailData(42u, 0u, 0u, string.Empty),
		new piceDetailData(48u, 0u, 0u, string.Empty),
		new piceDetailData(49u, 0u, 0u, string.Empty),
		new piceDetailData(50u, 0u, 0u, string.Empty),
		new piceDetailData(39u, 3u, 0u, string.Empty),
		new piceDetailData(76u, 0u, 0u, string.Empty),
		new piceDetailData(75u, 0u, 0u, string.Empty),
		new piceDetailData(72u, 0u, 0u, string.Empty),
		new piceDetailData(73u, 0u, 0u, string.Empty),
		new piceDetailData(83u, 0u, 0u, string.Empty),
		new piceDetailData(71u, 0u, 0u, string.Empty),
		new piceDetailData(71u, 0u, 0u, string.Empty),
		new piceDetailData(71u, 0u, 0u, string.Empty),
		new piceDetailData(101u, 0u, 0u, string.Empty),
		new piceDetailData(100u, 0u, 0u, string.Empty),
		new piceDetailData(118u, 0u, 0u, string.Empty),
		new piceDetailData(102u, 0u, 0u, string.Empty),
		new piceDetailData(103u, 0u, 0u, string.Empty),
		new piceDetailData(39u, 0u, 0u, string.Empty),
		new piceDetailData(40u, 0u, 0u, string.Empty),
		new piceDetailData(41u, 0u, 0u, string.Empty)
	};

	private List<piceDetailData> gs3_status_ext_bg_tbl_ = new List<piceDetailData>
	{
		new piceDetailData(58u, 0u, 0u, string.Empty),
		new piceDetailData(58u, 0u, 0u, string.Empty),
		new piceDetailData(59u, 0u, 0u, string.Empty),
		new piceDetailData(62u, 0u, 0u, string.Empty),
		new piceDetailData(63u, 2u, 0u, string.Empty),
		new piceDetailData(65u, 0u, 0u, string.Empty),
		new piceDetailData(87u, 0u, 0u, string.Empty),
		new piceDetailData(70u, 0u, 0u, string.Empty),
		new piceDetailData(88u, 0u, 0u, string.Empty),
		new piceDetailData(71u, 0u, 0u, string.Empty),
		new piceDetailData(89u, 0u, 0u, string.Empty),
		new piceDetailData(80u, 0u, 0u, string.Empty),
		new piceDetailData(93u, 0u, 0u, string.Empty),
		new piceDetailData(97u, 0u, 0u, string.Empty),
		new piceDetailData(99u, 0u, 0u, string.Empty),
		new piceDetailData(111u, 0u, 0u, string.Empty),
		new piceDetailData(112u, 0u, 0u, string.Empty),
		new piceDetailData(117u, 0u, 0u, string.Empty),
		new piceDetailData(118u, 0u, 0u, string.Empty),
		new piceDetailData(133u, 0u, 0u, string.Empty),
		new piceDetailData(124u, 0u, 0u, string.Empty),
		new piceDetailData(135u, 0u, 0u, string.Empty),
		new piceDetailData(134u, 0u, 0u, string.Empty),
		new piceDetailData(123u, 0u, 0u, string.Empty),
		new piceDetailData(122u, 0u, 0u, string.Empty),
		new piceDetailData(139u, 0u, 0u, string.Empty),
		new piceDetailData(140u, 0u, 0u, string.Empty),
		new piceDetailData(141u, 0u, 0u, string.Empty),
		new piceDetailData(142u, 2u, 0u, string.Empty),
		new piceDetailData(130u, 3u, 0u, string.Empty),
		new piceDetailData(145u, 0u, 0u, string.Empty),
		new piceDetailData(125u, 0u, 0u, string.Empty),
		new piceDetailData(126u, 0u, 0u, string.Empty),
		new piceDetailData(127u, 0u, 0u, string.Empty),
		new piceDetailData(128u, 0u, 0u, string.Empty),
		new piceDetailData(63u, 0u, 0u, string.Empty),
		new piceDetailData(64u, 0u, 0u, string.Empty),
		new piceDetailData(142u, 0u, 0u, string.Empty),
		new piceDetailData(135u, 0u, 0u, string.Empty),
		new piceDetailData(130u, 0u, 0u, string.Empty),
		new piceDetailData(131u, 0u, 0u, string.Empty),
		new piceDetailData(132u, 0u, 0u, string.Empty)
	};

	public const int NOTE_ITEM_ID_MASK = 16383;

	public const int NOTE_ST_MASK = 16384;

	public const int NOTE_ST_OPEN = 16384;

	public const int NOTE_ST_NO = 0;

	public const int NOTE_ID_MASK = 32768;

	public const int NOTE_ID_NAME = 32768;

	public const int NOTE_ID_ITEM = 0;

	public static piceDataCtrl instance
	{
		get
		{
			return instance_;
		}
	}

	public List<piceData> note_data
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_note_data_;
			case TitleId.GS2:
				return gs2_note_data_;
			case TitleId.GS3:
				return gs3_note_data_;
			default:
				return null;
			}
		}
	}

	public List<piceDetailData> status_ext_bg_tbl
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_status_ext_bg_tbl_;
			case TitleId.GS2:
				return gs2_status_ext_bg_tbl_;
			case TitleId.GS3:
				return gs3_status_ext_bg_tbl_;
			default:
				return gs1_status_ext_bg_tbl_;
			}
		}
	}

	private List<string> icon_file_
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_icon_file_;
			case TitleId.GS2:
				return gs2_icon_file_;
			case TitleId.GS3:
				return gs3_icon_file_;
			default:
				return null;
			}
		}
	}

	private List<string> icon_file_language_
	{
		get
		{
			switch (GSStatic.global_work_.title)
			{
			case TitleId.GS1:
				return gs1_icon_file_language_;
			case TitleId.GS2:
				return gs2_icon_file_language_;
			case TitleId.GS3:
				return gs3_icon_file_language_;
			default:
				return null;
			}
		}
	}

	private void Awake()
	{
		if (instance_ == null)
		{
			instance_ = this;
		}
	}

	public string GetIconItcFile(int in_note_no)
	{
		return "itc" + GetIconFile(in_note_no);
	}

	public string GetIconItFile(int in_note_no)
	{
		return "it" + GetIconFile(in_note_no);
	}

	private string GetIconFile(int in_note_no)
	{
		piceData piceData2 = note_data[in_note_no];
		if (piceData2.file_language_id != 255 && GSStatic.global_work_.language == "USA")
		{
			return icon_file_language_[piceData2.file_language_id] + "u";
		}
		return icon_file_[piceData2.file_id];
	}
}
