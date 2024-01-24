CREATE TABLE IF NOT EXISTS public.users
(
    created timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "createdBy" character varying(50) COLLATE pg_catalog."default" NOT NULL  DEFAULT 'a'::character varying,
    "isDeleted" boolean DEFAULT false,
    modified timestamp without time zone,
    "modifiedBy" character varying(50) COLLATE pg_catalog."default",    
    id uuid NOT NULL DEFAULT gen_random_uuid(),
    email character varying(50) COLLATE pg_catalog."default" NOT NULL,
    password character varying(80) COLLATE pg_catalog."default" NOT NULL,
    balance money NOT NULL,
    "roleId" uuid NOT NULL,
    CONSTRAINT pkey_users PRIMARY KEY (id),
    CONSTRAINT "fk_roleId" FOREIGN KEY ("roleId")
        REFERENCES public.roles (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)