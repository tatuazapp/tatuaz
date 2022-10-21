import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { FunctionComponent } from "react"
import styled from "styled-components"
import { TextBlockSlice } from "../../types.generated"

type TextBlockProps = SliceComponentProps<TextBlockSlice>

const TextBlock: FunctionComponent<TextBlockProps> = ({ slice }) => (
  <TextBlockWrapper>
    <TitleWrapper>
      {slice.primary.title && <PrismicRichText field={slice.primary.title} />}
    </TitleWrapper>
    {slice.primary.description && (
      <PrismicRichText field={slice.primary.description} />
    )}
  </TextBlockWrapper>
)

const TitleWrapper = styled.span`
  color: #8592e0;
`

const TextBlockWrapper = styled.section`
  max-width: 600px;
  margin: 4em auto;
  text-align: center;
`

export default TextBlock
