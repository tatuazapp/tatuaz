import { AxiosError } from "axios"
import { ErrorResponse } from "../api/tatuazApi"

export type ErrorApiResponse = AxiosError<ErrorResponse>

export type SignUpDtoErrorCode =
  | "UsernameNull"
  | "UsernameTooShort"
  | "UsernameTooLong"
  | "UsernameAlreadyInUse"
  | "UsernameInvalidCharacters"
  | "PhotoCategoryIdsNull"
  | "PhotoCategoryIdsTooFew"
  | "PhotoCategoryIdsTooMany"
  | "PhotoCategoryIdsInvalid"
  | "PhotoCategoryIdsDuplicate"

export type SetBioDtoErrorCode = "BioTooLong" | "CityTooLong"
