-- Criação da tabela clientes com campo cadastro_pendente
CREATE TABLE IF NOT EXISTS public.clientes
(
    id SERIAL PRIMARY KEY,
    usuario_id INTEGER NOT NULL UNIQUE,
    cpf VARCHAR(14) UNIQUE,
    telefone VARCHAR(15),
    data_nascimento DATE,
    cep VARCHAR(9),
    endereco VARCHAR(200),
    numero VARCHAR(10),
    complemento VARCHAR(100),
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado CHAR(2),
    cadastro_pendente BOOLEAN DEFAULT TRUE, -- NOVO CAMPO
    
    CONSTRAINT fk_clientes_usuarios 
        FOREIGN KEY (usuario_id) 
        REFERENCES public.usuarios(id)
        ON DELETE CASCADE
);

-- Definindo o proprietário da tabela
ALTER TABLE public.clientes OWNER TO postgres;

-- Comentários para documentação
COMMENT ON TABLE public.clientes IS 'Tabela de clientes com dados complementares';
COMMENT ON COLUMN public.clientes.cadastro_pendente IS 'Indica se o cadastro está pendente de complementação (true) ou completo (false)';

-- Índices para melhor performance
CREATE INDEX IF NOT EXISTS idx_clientes_usuario_id ON public.clientes(usuario_id);
CREATE INDEX IF NOT EXISTS idx_clientes_cpf ON public.clientes(cpf);
CREATE INDEX IF NOT EXISTS idx_clientes_telefone ON public.clientes(telefone);
CREATE INDEX IF NOT EXISTS idx_clientes_cadastro_pendente ON public.clientes(cadastro_pendente);