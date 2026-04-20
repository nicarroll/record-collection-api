CREATE TABLE  IF NOT EXISTS public.records (
    id SERIAL PRIMARY KEY,
    artist_name TEXT NOT NULL,
    album_title TEXT NOT NULL,
    release_year INT NOT NULL,
    discogs_id BIGINT
);