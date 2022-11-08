import { FormattedMessage } from "react-intl"
import PhotoCard from "../PhotoCard"
import { CardsContainer, TitleContainer } from "./styles"

const PhotoCardSection = () => (
  <>
    <FormattedMessage
      defaultMessage="{title}"
      description="PhotoCard section title "
      id="P66lTc"
      values={{
        title: "Najpopularniejsi w tym miesiącu",
      }}
    >
      {(title) => <TitleContainer>{title}</TitleContainer>}
    </FormattedMessage>
    <CardsContainer>
      {/* MOCK_START */}
      <PhotoCard
        description="Szkocka whisky, typu blended, produkowana przez Chivas Brothers"
        imageURL="https://i.ytimg.com/vi/65sx5RlQRhs/maxresdefault.jpg"
      />
      <PhotoCard
        description="Szkocka whisky, typu blended, produkowana przez Chivas Fathers"
        imageURL="https://itsastampede615821596.files.wordpress.com/2021/12/how-many-sonic-cartoons-are-there.jpg?w=600"
      />

      <PhotoCard
        description="Szkocka whisky, typu blended, produkowana przez Chivas Mothers"
        imageURL="https://www.looper.com/img/gallery/the-untold-truth-of-knuckles-from-sonic/l-intro-1630433466.jpg"
      />

      <PhotoCard
        description="Szkocka whisky, typu blended, produkowana przez Chivas Sisters"
        imageURL="https://i.ytimg.com/vi/AQbFQnsa0a4/maxresdefault.jpg"
      />
      {/* MOCK_END */}
    </CardsContainer>
  </>
)

export default PhotoCardSection
