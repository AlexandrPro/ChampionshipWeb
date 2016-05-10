-- MySQL Script generated by MySQL Workbench
-- 05/08/16 19:29:08
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema championship
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema championship
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `championship` DEFAULT CHARACTER SET utf8 ;
USE `championship` ;

-- -----------------------------------------------------
-- Table `championship`.`coach`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`coach` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`citys`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`citys` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name_ru` VARCHAR(100) NOT NULL,
  `name_ua` VARCHAR(100) NOT NULL,
  `name_en` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`teams`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`teams` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `short_name` VARCHAR(10) NOT NULL,
  `name` VARCHAR(200) NULL,
  `coach_id` INT NULL,
  `citys_id` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_teams_coach1_idx` (`coach_id` ASC),
  INDEX `fk_teams_citys1_idx` (`citys_id` ASC),
  CONSTRAINT `fk_teams_coach1`
    FOREIGN KEY (`coach_id`)
    REFERENCES `championship`.`coach` (`id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `fk_teams_citys1`
    FOREIGN KEY (`citys_id`)
    REFERENCES `championship`.`citys` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`position`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`position` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name_ru` VARCHAR(25) NOT NULL,
  `name_ua` VARCHAR(25) NOT NULL,
  `name_en` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`player_state`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`player_state` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name_ru` VARCHAR(20) NOT NULL,
  `name_ua` VARCHAR(20) NOT NULL,
  `name_en` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`players`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`players` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `team_id` INT NULL,
  `date_of_birth` DATE NULL,
  `weight` INT NULL,
  `height` INT NULL,
  `position_id` INT NULL,
  `photo_path` VARCHAR(200) NULL,
  `player_state_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `team_idx` (`team_id` ASC),
  INDEX `player_position_FK_idx` (`position_id` ASC),
  INDEX `fk_players_player_state1_idx` (`player_state_id` ASC),
  CONSTRAINT `player_team_FK`
    FOREIGN KEY (`team_id`)
    REFERENCES `championship`.`teams` (`id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `player_position_FK`
    FOREIGN KEY (`position_id`)
    REFERENCES `championship`.`position` (`id`)
    ON DELETE SET NULL
    ON UPDATE SET NULL,
  CONSTRAINT `fk_players_player_state`
    FOREIGN KEY (`player_state_id`)
    REFERENCES `championship`.`player_state` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`game_state`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`game_state` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name_ru` VARCHAR(20) NOT NULL,
  `name_ua` VARCHAR(20) NOT NULL,
  `name_en` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`championship`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`championship` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`type_of_stage`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`type_of_stage` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name_ru` VARCHAR(20) NOT NULL,
  `name_ua` VARCHAR(20) NOT NULL,
  `name_en` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`stage`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`stage` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `championship_id` INT NOT NULL,
  `type_of_stage_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_stage_championship1_idx` (`championship_id` ASC),
  INDEX `fk_stage_type_of_stage1_idx` (`type_of_stage_id` ASC),
  CONSTRAINT `fk_stage_championship1`
    FOREIGN KEY (`championship_id`)
    REFERENCES `championship`.`championship` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_stage_type_of_stage1`
    FOREIGN KEY (`type_of_stage_id`)
    REFERENCES `championship`.`type_of_stage` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`games`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`games` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `date` DATE NULL,
  `team1_id` INT NULL,
  `team2_id` INT NULL,
  `team1_score` INT NULL,
  `team2_score` INT NULL,
  `place` VARCHAR(200) NULL,
  `game_state_id` INT NOT NULL,
  `stage_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `game_team1_FK_idx` (`team1_id` ASC),
  INDEX `game_team2_FK_idx` (`team2_id` ASC),
  INDEX `fk_games_game_state1_idx` (`game_state_id` ASC),
  INDEX `fk_games_stage1_idx` (`stage_id` ASC),
  CONSTRAINT `game_team1_FK`
    FOREIGN KEY (`team1_id`)
    REFERENCES `championship`.`teams` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `game_team2_FK`
    FOREIGN KEY (`team2_id`)
    REFERENCES `championship`.`teams` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_games_game_state`
    FOREIGN KEY (`game_state_id`)
    REFERENCES `championship`.`game_state` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `fk_games_stage`
    FOREIGN KEY (`stage_id`)
    REFERENCES `championship`.`stage` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`statistics`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`statistics` (
  `games_id` INT NOT NULL,
  `players_id` INT NOT NULL,
  `free_throws_attempts` INT NOT NULL,
  `free_throws_made` INT NOT NULL,
  `two_point_throws_attempts` INT NOT NULL,
  `two_point_throws_made` INT NOT NULL,
  `three_point_throws_attempts` INT NOT NULL,
  `three_point_throws_made` INT NOT NULL,
  `total_score` INT GENERATED ALWAYS AS ((free_throws_made + (two_point_throws_made * 2) + (three_point_throws_made * 3))) VIRTUAL,
  `offensive_rebounds` INT NOT NULL,
  `deffensive_rebounds` INT NOT NULL,
  `assists` INT NOT NULL,
  `steals` INT NOT NULL,
  `turnovers` INT NOT NULL,
  `blocked_shots` INT NOT NULL,
  `commited_fouls` INT NOT NULL,
  `recieved_fouls` INT NOT NULL,
  PRIMARY KEY (`games_id`, `players_id`),
  INDEX `fk_games_has_players_players1_idx` (`players_id` ASC),
  INDEX `fk_games_has_players_games1_idx` (`games_id` ASC),
  CONSTRAINT `fk_games_has_players_games1`
    FOREIGN KEY (`games_id`)
    REFERENCES `championship`.`games` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `fk_games_has_players_players1`
    FOREIGN KEY (`players_id`)
    REFERENCES `championship`.`players` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`judges`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`judges` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`judge_in_game`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`judge_in_game` (
  `games_id` INT NOT NULL,
  `judge_id` INT NOT NULL,
  PRIMARY KEY (`games_id`, `judge_id`),
  INDEX `fk_games_has_judge_judge1_idx` (`judge_id` ASC),
  INDEX `fk_games_has_judge_games1_idx` (`games_id` ASC),
  CONSTRAINT `fk_games_has_judge_games1`
    FOREIGN KEY (`games_id`)
    REFERENCES `championship`.`games` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_games_has_judge_judge1`
    FOREIGN KEY (`judge_id`)
    REFERENCES `championship`.`judges` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`teams_in_stage`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`teams_in_stage` (
  `teams_id` INT NOT NULL,
  `stage_id` INT NOT NULL,
  `teamscore` INT NULL,
  PRIMARY KEY (`teams_id`, `stage_id`),
  INDEX `fk_teams_has_stage_stage1_idx` (`stage_id` ASC),
  INDEX `fk_teams_has_stage_teams1_idx` (`teams_id` ASC),
  CONSTRAINT `fk_teams_has_stage_teams1`
    FOREIGN KEY (`teams_id`)
    REFERENCES `championship`.`teams` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_teams_has_stage_stage1`
    FOREIGN KEY (`stage_id`)
    REFERENCES `championship`.`stage` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`roles` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `championship`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `championship`.`users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `role` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_user_post_idx` (`role` ASC),
  CONSTRAINT `fk_user_role`
    FOREIGN KEY (`role`)
    REFERENCES `championship`.`roles` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
