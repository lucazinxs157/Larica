-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Tempo de geração: 24-Out-2023 às 01:11
-- Versão do servidor: 8.0.31
-- versão do PHP: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `laricao`
--
CREATE DATABASE IF NOT EXISTS `laricao` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `laricao`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `amburge`
--

DROP TABLE IF EXISTS `amburge`;
CREATE TABLE IF NOT EXISTS `amburge` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  `preco` decimal(7,2) NOT NULL,
  `foto` varchar(100) NOT NULL DEFAULT 'E:\\Laricão Pizzaria\\src/default.png',
  `categoria` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `amburge`
--

INSERT INTO `amburge` (`id`, `nome`, `preco`, `foto`, `categoria`) VALUES
(2, 'Americano', '40.00', 'E:\\laricao\\img\\americano.jpg', 'Hamburge'),
(3, 'Cheese Bacon', '42.00', 'E:\\laricao\\img\\Cheese bacon.jpg', 'Hamburge'),
(6, 'Classico', '39.00', 'E:\\laricao\\img\\classic.jpg', 'Hamburge'),
(7, 'Cream Cheese', '37.00', 'E:\\laricao\\img\\cream_cheese.jpg', 'Hamburge'),
(8, 'Costela', '42.00', 'E:\\laricao\\img\\costela.jpg', 'Hamburge'),
(9, 'Junior', '30.00', 'E:\\laricao\\img\\junior.jpg', 'Hamburge'),
(10, 'Picles', '38.00', 'E:\\laricao\\img\\picles.jpg', 'Hamburge'),
(12, 'Pão de alho', '5.00', 'E:\\laricao\\img\\pãodalho.jpg', 'Porções'),
(13, 'Batata canoa', '10.00', 'E:\\laricao\\img\\batata_canoa.jpg', 'Porções'),
(14, 'Batata frita', '20.00', 'E:\\laricao\\img\\batata_frita.jpg', 'Porções'),
(15, 'Batata especial', '30.00', 'E:\\laricao\\img\\batata_especial.jpg', 'Porções'),
(16, 'Anel de cebola', '20.00', 'E:\\laricao\\img\\Anel_cb.jpg', 'Porções'),
(17, 'Mini coxinhas', '25.00', 'E:\\laricao\\img\\Coxinha.jpg', 'Porções'),
(18, 'Brownie', '7.00', 'E:\\Laricao\\img\\brownie.jpg', 'Sobremesas'),
(19, 'Cookies', '5.00', 'E:\\Laricao\\img\\kuk.jpg', 'Sobremesas'),
(20, 'Donuts', '15.00', 'E:\\Laricao\\img\\dunuts.jpg', 'Sobremesas'),
(21, 'Agua C/G', '4.00', 'E:\\Laricao\\img\\agua.jpg', 'Bebidas'),
(22, 'Agua S/G', '4.00', 'E:\\Laricao\\img\\agua-Sc.jpg\r\n', 'Bebidas'),
(23, 'Chopp de vinho', '10.00', 'E:\\Laricao\\img\\chop.jpg', 'Bebidas'),
(24, 'Coca', '6.00', 'E:\\Laricao\\img\\coca.jpg', 'Bebidas'),
(25, 'Coca 600ml', '8.00', 'E:\\Laricao\\img\\coca600.jpg', 'Bebidas'),
(26, 'Coca Zero', '6.00', 'E:\\Laricao\\img\\cocaZero.jpg', 'Bebidas'),
(28, 'Dell vale', '7.00', 'E:\\Laricao\\img\\suco.jpg', 'Bebidas'),
(29, 'Fanta laranja', '6.00', 'E:\\Laricao\\img\\fanta-laranja.jpg', 'Bebidas'),
(30, 'Fanta uva', '6.00', 'E:\\Laricao\\img\\fanta-uva.jpg', 'Bebidas'),
(31, 'Guaraná lata', '6.00', 'E:\\Laricao\\img\\guarana.jpg', 'Bebidas'),
(32, 'Guaraná 600ml ', '6.00', 'E:\\Laricao\\img\\guarana600.jpg', 'Bebidas'),
(33, 'Pepsi', '6.00', 'E:\\Laricao\\img\\pepsi.jpg', 'Bebidas'),
(34, 'Tubaina', '6.00', 'E:\\Laricao\\img\\tubaina.jpg', 'Bebidas'),
(1, 'regulador', '0.00', 'E:\\Laricao\\img/default.png', '');

-- --------------------------------------------------------

--
-- Estrutura da tabela `carrinho`
--

DROP TABLE IF EXISTS `carrinho`;
CREATE TABLE IF NOT EXISTS `carrinho` (
  `cod_prod` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`cod_prod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `pedidos`
--

DROP TABLE IF EXISTS `pedidos`;
CREATE TABLE IF NOT EXISTS `pedidos` (
  `id_pedido` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `idusuario` int NOT NULL,
  `cep` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `bairro` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `endereco` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `num` varchar(10) NOT NULL,
  `tel` varchar(15) NOT NULL,
  `status` varchar(50) NOT NULL,
  `pedido` varchar(500) NOT NULL,
  `obs` varchar(300) NOT NULL,
  PRIMARY KEY (`id_pedido`)
) ENGINE=MyISAM AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `perfil`
--

DROP TABLE IF EXISTS `perfil`;
CREATE TABLE IF NOT EXISTS `perfil` (
  `id_perfil` int NOT NULL AUTO_INCREMENT,
  `perfil` varchar(60) NOT NULL,
  PRIMARY KEY (`id_perfil`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `perfil`
--

INSERT INTO `perfil` (`id_perfil`, `perfil`) VALUES
(1, 'usuario'),
(2, 'administrador');

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `idusuario` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(60) DEFAULT NULL,
  `sobrenome` varchar(100) NOT NULL,
  `cpf` varchar(14) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `telefone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `senha` varchar(42) DEFAULT NULL,
  `id_perfil` int NOT NULL,
  `email` varchar(75) NOT NULL,
  PRIMARY KEY (`idusuario`),
  KEY `FK_perfil` (`id_perfil`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`idusuario`, `nome`, `sobrenome`, `cpf`, `telefone`, `senha`, `id_perfil`, `email`) VALUES
(2, 'Duardo', 'Sampaio', '139.167.549-55', '(41) 95555-5555', 'e8c0653fea13f91bf3c48159f7c24f78', 2, 'sampaioeeduardo36@gmail.com');

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `FK_perfil` FOREIGN KEY (`id_perfil`) REFERENCES `perfil` (`id_perfil`);
--
-- Banco de dados: `testando`
--
CREATE DATABASE IF NOT EXISTS `testando` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `testando`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `perfil`
--

DROP TABLE IF EXISTS `perfil`;
CREATE TABLE IF NOT EXISTS `perfil` (
  `id_perfil` int NOT NULL AUTO_INCREMENT,
  `perfil` varchar(60) NOT NULL,
  PRIMARY KEY (`id_perfil`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `perfil`
--

INSERT INTO `perfil` (`id_perfil`, `perfil`) VALUES
(1, 'usuario'),
(2, 'administrador');

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto`
--

DROP TABLE IF EXISTS `produto`;
CREATE TABLE IF NOT EXISTS `produto` (
  `cod_prod` int NOT NULL AUTO_INCREMENT,
  `desc_prod` varchar(60) DEFAULT NULL,
  `preco_prod` decimal(7,2) DEFAULT NULL,
  `qtde_prod` int DEFAULT NULL,
  `perecivel` tinyint(1) DEFAULT NULL,
  `dat_validade` date DEFAULT NULL,
  `foto` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`cod_prod`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `produto`
--

INSERT INTO `produto` (`cod_prod`, `desc_prod`, `preco_prod`, `qtde_prod`, `perecivel`, `dat_validade`, `foto`) VALUES
(1, 'Coca cola 500 ml', '6.70', 1, 0, '2023-08-14', 'E:\\dsnoite\\src/coquinha geladinha hmm.png');

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `idusuario` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(60) DEFAULT NULL,
  `senha` varchar(42) DEFAULT NULL,
  `id_perfil` int NOT NULL,
  `email` varchar(75) NOT NULL,
  PRIMARY KEY (`idusuario`),
  KEY `FK_perfil` (`id_perfil`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`idusuario`, `nome`, `senha`, `id_perfil`, `email`) VALUES
(2, 'jose', '6cfe0e6127fa25df2a0ef2ae1067d915', 2, 'sampaioeeduardo36@gmail.com'),
(3, 'amanda', 'a123', 2, '');

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `FK_perfil` FOREIGN KEY (`id_perfil`) REFERENCES `perfil` (`id_perfil`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
