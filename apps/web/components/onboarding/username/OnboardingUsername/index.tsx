import { Center, FormControl, FormErrorMessage } from "@chakra-ui/react"
import { FunctionComponent } from "react"
import { UseFormReturn } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { OnboardingTitle } from "../../OnboardingTitle/styles"
import { InputWithErrorWrapper, InputWrapper, UsernameWrapper } from "./styles"
import { useOnboardingUsernameValidationRules } from "./validation"

export type OnboardingUsernameFormValues = {
  username: string
}

type OnboardingUsernameProps = {
  children?: React.ReactNode
  onNext: () => void
  form: UseFormReturn<OnboardingUsernameFormValues>
}

const OnboardingUsername: FunctionComponent<OnboardingUsernameProps> = ({
  onNext,
  form: {
    register,
    handleSubmit,
    formState: { errors },
  },
}) => {
  const intl = useIntl()

  const usernameValidationRules = useOnboardingUsernameValidationRules()

  return (
    <>
      <Center>
        <OnboardingTitle>
          <FormattedMessage
            defaultMessage="Jak chcesz się nazywać?"
            id="FK5hPZ"
          />
        </OnboardingTitle>
      </Center>
      <form onSubmit={handleSubmit(onNext)}>
        <FormControl isInvalid={!!errors.username}>
          <UsernameWrapper>
            <InputWithErrorWrapper>
              <InputWrapper
                color="secondary"
                focusBorderColor="primary.100"
                placeholder={intl.formatMessage({
                  defaultMessage: "Nazwa użytkownika",
                  id: "lWVNV+",
                })}
                size="lg"
                underlineColor="white.100"
                variant="flushed"
                {...register("username", usernameValidationRules)}
              />
              <FormErrorMessage>
                {errors.username && errors.username.message}
              </FormErrorMessage>
            </InputWithErrorWrapper>
          </UsernameWrapper>
        </FormControl>
      </form>
    </>
  )
}

export default OnboardingUsername
