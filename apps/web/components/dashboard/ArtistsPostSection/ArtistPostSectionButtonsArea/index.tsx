import { Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import { SearchPostsFlag } from "../../../../api/tatuazApi"
import { theme } from "../../../../styles/theme"
import {
  ArtistsPostSectionButtonAreaWrapper,
  TypeButton,
  TypeButtonsContainer,
} from "./styles"

const mapSearchPostsFlagToString = (flag: SearchPostsFlag) => {
  switch (flag) {
    case SearchPostsFlag.All:
      return <FormattedMessage defaultMessage="Wszystkie" id="+Bww4z" />
    case SearchPostsFlag.OnlyPhotos:
      return <FormattedMessage defaultMessage="Zdjęcia" id="GgQAS6" />
    case SearchPostsFlag.OnlyPosts:
      return <FormattedMessage defaultMessage="Posty" id="jHbya6" />
  }
}

type ArtistsPostSectionButtonAreaProps = {
  selectedType: SearchPostsFlag
  setSelectedType: (type: SearchPostsFlag) => void
}

const ArtistsPostSectionButtonArea: FunctionComponent<
  ArtistsPostSectionButtonAreaProps
> = ({ selectedType, setSelectedType }) => {
  const buttonTypes = [
    SearchPostsFlag.All,
    SearchPostsFlag.OnlyPhotos,
    SearchPostsFlag.OnlyPosts,
  ]

  return (
    <ArtistsPostSectionButtonAreaWrapper>
      <TypeButtonsContainer>
        {buttonTypes.map((buttonType) => (
          <TypeButton
            key={buttonType}
            isSelected={selectedType === buttonType}
            onClick={() => {
              setSelectedType(buttonType)
            }}
          >
            <Paragraph
              color={
                selectedType === buttonType
                  ? theme.colors.background1
                  : theme.colors.secondary
              }
            >
              {mapSearchPostsFlagToString(buttonType)}
            </Paragraph>
          </TypeButton>
        ))}
      </TypeButtonsContainer>
    </ArtistsPostSectionButtonAreaWrapper>
  )
}

export default ArtistsPostSectionButtonArea
