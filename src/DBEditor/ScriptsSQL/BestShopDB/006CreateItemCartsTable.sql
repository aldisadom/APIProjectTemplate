CREATE TABLE IF NOT EXISTS public."itemsCarts"
(
    "cartId" uuid NOT NULL,
    "itemId" uuid NOT NULL,
    quantity integer NOT NULL,
    CONSTRAINT "fk_cartId" FOREIGN KEY ("cartId")
        REFERENCES public.carts (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT "fk_itemId" FOREIGN KEY ("itemId")
        REFERENCES public.items (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)