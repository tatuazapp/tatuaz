import * as prismicH from "@prismicio/helpers"
import { PrismicNextImage } from "@prismicio/next"
import { SliceComponentProps } from "@prismicio/react"
import React, { FunctionComponent } from "react"
import { ImageSlice } from "../../types.generated"

type ImageProps = SliceComponentProps<ImageSlice>

const Image: FunctionComponent<ImageProps> = ({
  slice: {
    primary: { image },
  },
}) => (
  <section>
    {prismicH.isFilled.image(image) && (
      <div>
        <PrismicNextImage field={image} layout="responsive" />
      </div>
    )}
  </section>
)

export default Image
