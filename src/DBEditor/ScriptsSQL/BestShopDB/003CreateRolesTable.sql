CREATE TABLE IF NOT EXISTS public.roles
(
    created timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "createdBy" character varying(50) COLLATE pg_catalog."default" NOT NULL  DEFAULT 'a'::character varying,
    "isDeleted" boolean DEFAULT false,
    modified timestamp without time zone,
    "modifiedBy" character varying(50) COLLATE pg_catalog."default",    
    id uuid NOT NULL DEFAULT gen_random_uuid(),
    name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT roles_pkey PRIMARY KEY (id)
)