import { ErrorResponse } from "../api/tatuazApi"

export type ErrorApiResponse = {
  error: ErrorResponse
}

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
