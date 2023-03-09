import { Flex, Spinner, useToast } from "@chakra-ui/react"
import { useMutation } from "@tanstack/react-query"
import { useRouter } from "next/router"
import { FunctionComponent, useCallback } from "react"
import { UseFormReturn } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { useTheme } from "styled-components"
import { api } from "../../../../api/apiClient"
import { SignUpDto } from "../../../../api/tatuazApi"
import {
  ErrorApiResponse,
  SignUpDtoErrorCode,
} from "../../../../types/apiErrors"
import ApiErrorHandler from "../../../../utils/errors/ApiErrorHandler"
import { BottomArrowButton } from "../../../common/buttons/BottomArrowButton/styles"
import { OnboardingUsernameFormValues } from "../../username/OnboardingUsername"

type OnboardingPreferencesFooterButtonsProps = {
  onBack: () => void
  usernameForm: UseFormReturn<OnboardingUsernameFormValues>
  selectedPreferences: number[]
}

const OnboardingPreferencesFooterButtons: FunctionComponent<
  OnboardingPreferencesFooterButtonsProps
> = ({ usernameForm, onBack, selectedPreferences }) => {
  const intl = useIntl()
  const toast = useToast()
  const theme = useTheme()
  const router = useRouter()

  const mutation = useMutation({
    mutationFn: (data: SignUpDto) => api.identity.signUp(data),
    onError: (res: ErrorApiResponse) => {
      const handler = new ApiErrorHandler<SignUpDtoErrorCode>(res.error)

      handler
        .handle("UsernameAlreadyInUse", () => {
          usernameForm.setError("username", {
            type: "manual",
            message: intl.formatMessage({
              defaultMessage: "Nazwa użytkownika jest już zajęta",
              id: "m9opXW",
            }),
          })

          onBack()
        })
        .handle("*", () => {
          toast({
            title: intl.formatMessage({
              defaultMessage:
                "Wystąpił błąd podczas rejestracji. Spróbuj ponownie później",
              id: "q/txt0",
            }),
            status: "error",
            position: "top",
          })
        })
        .run()
    },
    onSuccess: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Rejestracja zakończona pomyślnie",
          id: "MSnKM4",
        }),
        status: "success",
        position: "top",
      })

      router.push("/")
    },
  })

  const signUp = useCallback(async () => {
    await mutation.mutateAsync({
      username: usernameForm.getValues().username,
      categoryIds: selectedPreferences,
    })
  }, [mutation, selectedPreferences, usernameForm])

  return (
    <>
      <BottomArrowButton direction="left" variant="secondary" onClick={onBack}>
        <FormattedMessage defaultMessage="Wstecz" id="xX+nrs" />
      </BottomArrowButton>
      <BottomArrowButton
        disabled={mutation.isLoading || selectedPreferences.length < 3}
        onClick={signUp}
      >
        <Flex alignItems="center" gap={theme.space.xsmall}>
          <FormattedMessage defaultMessage="Zakończ" id="v0lTFF" />
          {mutation.isLoading && <Spinner size="sm" />}
        </Flex>
      </BottomArrowButton>
    </>
  )
}

export default OnboardingPreferencesFooterButtons
