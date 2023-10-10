-- Створення таблиці user
CREATE TABLE user (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    coins_amount INTEGER,
    password VARCHAR(255) NOT NULL
);

-- Створення таблиці task
CREATE TABLE task (
    task_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    date_of_creation TIMESTAMP,
    expected_finish_time TIMESTAMP,
    finish_time TIMESTAMP,
    priority INTEGER,
    completed BOOLEAN,
    execution_time INTERVAL,
    user_id INTEGER REFERENCES user(user_id)
);

-- Створення таблиці sounds
CREATE TABLE sounds (
    sound_id SERIAL PRIMARY KEY,
    sound_name VARCHAR(255) NOT NULL,
    file_path VARCHAR(255)
);
