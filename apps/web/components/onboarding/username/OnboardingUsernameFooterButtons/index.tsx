import { FunctionComponent } from "react"
import { UseFormReturn } from "react-hook-form"
import { FormattedMessage } from "react-intl"
import useIsMobile from "../../../../utils/hooks/useIsMobile"
import { BottomArrowButton } from "../../../common/buttons/BottomArrowButton/styles"
import { OnboardingUsernameFormValues } from "../OnboardingUsername"

type OnboardingUsernameFooterButtonsProps = {
  onNext: () => void
  form: UseFormReturn<OnboardingUsernameFormValues>
}

const OnboardingUsernameFooterButtons: FunctionComponent<
  OnboardingUsernameFooterButtonsProps
> = ({ onNext, form }) => {
  const isMobile = useIsMobile()

  return (
    <BottomArrowButton
      disabled={!!form.formState.errors.username || !form.formState.isDirty}
      maxWidth={isMobile ? 1000 : undefined} // allow button to be full width on mobile
      onClick={onNext}
    >
      <FormattedMessage defaultMessage="Dalej" id="+N4HKT" />
    </BottomArrowButton>
  )
}

export default OnboardingUsernameFooterButtons
