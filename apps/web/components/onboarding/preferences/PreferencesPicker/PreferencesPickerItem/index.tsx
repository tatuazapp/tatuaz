import { Fade } from "@chakra-ui/react"
import { Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { CheckIcon, IconWrapper, PreferencesPickerItemWrapper } from "./styles"

type PreferencesPickerItemProps = {
  title: string
  imageUrl: string
  onClick: () => void
  active?: boolean
}

const PreferencesPickerItem: FunctionComponent<PreferencesPickerItemProps> = ({
  title,
  imageUrl,
  onClick,
  active = false,
}) => (
  <PreferencesPickerItemWrapper>
    <IconWrapper
      as="button"
      checked={active}
      imageUrl={imageUrl}
      onClick={onClick}
    >
      <Fade unmountOnExit in={active}>
        <CheckIcon />
      </Fade>
    </IconWrapper>
    <Paragraph level={2}>{title}</Paragraph>
  </PreferencesPickerItemWrapper>
)

export default PreferencesPickerItem
