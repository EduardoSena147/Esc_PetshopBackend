CREATE TABLE pets (
    id SERIAL PRIMARY KEY,
    cliente_id INT NOT NULL,
    tipo_animal_id INT NOT NULL,
    
    nome VARCHAR(100) NOT NULL,
    raca VARCHAR(100),
    cor VARCHAR(50),
    sexo CHAR(1) CHECK (sexo IN ('M', 'F')), -- M = macho, F = fêmea
    data_nascimento DATE,
    peso DECIMAL(5,2), -- em kg
    porte VARCHAR(20), -- pequeno, médio, grande
    castrado BOOLEAN DEFAULT FALSE,
    observacoes TEXT,
    
    data_cadastro TIMESTAMP DEFAULT NOW(),
    ativo BOOLEAN DEFAULT TRUE,
    
    CONSTRAINT fk_pet_cliente FOREIGN KEY (cliente_id) REFERENCES clientes (id),
    CONSTRAINT fk_pet_tipo_animal FOREIGN KEY (tipo_animal_id) REFERENCES tipo_animais (id)
);

ALTER TABLE pets 
ALTER COLUMN data_cadastro TYPE TIMESTAMP WITH TIME ZONE 
USING data_cadastro AT TIME ZONE 'UTC';