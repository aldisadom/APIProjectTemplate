CREATE TABLE IF NOT EXISTS public.carts
(
    created timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    modified timestamp without time zone, 
    id uuid NOT NULL DEFAULT gen_random_uuid(),
    "userId" uuid NOT NULL,
    "isPurchased" bool NOT NULL DEFAULT FALSE,
    price money NOT NULL,
    CONSTRAINT pkey_carts PRIMARY KEY (id)
)