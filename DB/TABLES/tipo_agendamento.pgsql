-- 1. Primeiro cria a sequência se não existir
CREATE SEQUENCE IF NOT EXISTS public.tipo_agendamento_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

-- 2. Cria a tabela
CREATE TABLE IF NOT EXISTS public.tipo_agendamento
(
    id integer NOT NULL DEFAULT nextval('tipo_agendamento_id_seq'::regclass),
    descricao character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT tipo_agendamento_pkey PRIMARY KEY (id),
    CONSTRAINT tipo_agendamento_descricao_key UNIQUE (descricao)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.tipo_agendamento
    OWNER to postgres;

COMMENT ON TABLE public.tipo_agendamento
    IS 'Tabela de tipos de agendamentos (Banho, tosa e banho/tosa)';

COMMENT ON COLUMN public.tipo_agendamento.id
    IS 'Identificador único do tipo de agendamento';

COMMENT ON COLUMN public.tipo_agendamento.descricao
    IS 'Descrição do tipo de agendamento';