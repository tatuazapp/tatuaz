import { useIntl } from "react-intl"

export const useOnboardingUsernameValidationRules = () => {
  const intl = useIntl()

  return {
    required: intl.formatMessage({
      defaultMessage: "Nazwa użytkownika jest wymagana",
      id: "emReSd",
    }),
    pattern: {
      value: /^[a-zA-Z0-9_]*$/,
      message: intl.formatMessage({
        defaultMessage:
          "Nazwa użytkownika może zawierać tylko litery, cyfry i znak podkreślenia",
        id: "tW7mIK",
      }),
    },
    minLength: {
      value: 4,
      message: intl.formatMessage({
        defaultMessage: "Nazwa użytkownika musi mieć co najmniej 4 znaki",
        id: "1x5ycw",
      }),
    },
    maxLength: {
      value: 32,
      message: intl.formatMessage({
        defaultMessage: "Nazwa użytkownika może mieć maksymalnie 32 znaki",
        id: "VkDHW9",
      }),
    },
  }
}
