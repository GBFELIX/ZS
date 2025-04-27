create database ZSDB;

use ZSDB;

CREATE TABLE EstoqueItems (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Quantidade INT NOT NULL,
    Valor DECIMAL(10 , 2 ) NOT NULL
);